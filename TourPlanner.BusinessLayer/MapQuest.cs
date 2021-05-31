using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TourPlanner.BusinessLayer {
    public class MapQuest
    {
        private static readonly string BaseURL = "https://www.mapquestapi.com";
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _apiKey = ConfigurationManager.AppSettings["MapQuestKey"];
        private static readonly string _filePath = ConfigurationManager.AppSettings["ImagePath"];

        public static string LoadImage(string fromLocation, string toLocation)
        {
            if (DoesLocationExist(fromLocation) && DoesLocationExist(toLocation))
            {

                var url = BaseURL + "/staticmap/v5/map?start=" + fromLocation + "&end=" + toLocation + "&size=600,400@2x&key=" + _apiKey;
                var fileName = GetUniqueFilename();
                var fullFilePath = _filePath + fileName + ".jpg";
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(url), fullFilePath);
                    client.Dispose();
                }
                return fullFilePath;
            }

            return "";
        }

        private static string GetUniqueFilename()
        {
            DateTime dt = DateTime.Now;
            var checkName = dt.ToString("yyyyMMdd");
            var fileIndex = 1;
            
            string[] files = Directory.GetFiles(_filePath);
            foreach (var fileName in files)
            {
                if (fileName.Contains(checkName))
                {
                    var split = fileName.Split(checkName + "_");
                    var checkNumber = Int32.Parse(split[1].Split(".jpg")[0]);
                    if (checkNumber >= fileIndex)
                        fileIndex = checkNumber + 1;
                }
            }

            return checkName+"_"+fileIndex;
        }

        public static bool DoesLocationExist(string location)
        {
            var task = Task.Run(() => _client.GetAsync(BaseURL + "/geocoding/v1/address?key=" + _apiKey + "&location=" + location));
            task.Wait();

            var stringJsonResponse = task.Result.Content.ReadAsStringAsync().Result;

            JObject jSonResponse = JObject.Parse(stringJsonResponse);

            if (jSonResponse["results"]?[0]?["locations"]?.Count() > 1)
            {
                return true;
            }

            return false;
        }
    }
}
