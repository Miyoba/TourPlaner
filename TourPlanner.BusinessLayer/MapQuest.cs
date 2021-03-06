﻿using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TourPlanner.BusinessLayer {
    public class MapQuest : IMapQuest
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;
        private readonly string _apiKey;
        private readonly string _filePath;

        public MapQuest()
        {
            _baseUrl = "https://www.mapquestapi.com";
            _client = new HttpClient();
            _apiKey = ConfigurationManager.AppSettings["MapQuestKey"];
            _filePath = ConfigurationManager.AppSettings["ImagePath"];
        }

        public string LoadImage(string fromLocation, string toLocation)
        {
            if (DoesLocationExist(fromLocation) && DoesLocationExist(toLocation))
            {

                var url = _baseUrl + "/staticmap/v5/map?start=" + fromLocation + "&end=" + toLocation + "&size=600,400@2x&key=" + _apiKey;
                var fileName = GetUniqueFilename();
                var fullFilePath = _filePath + fileName + ".jpg";
                using (WebClient client = new WebClient())
                {
                    //client.DownloadFile(new Uri(url), fullFilePath);
                    //client.Dispose();
                    var data = client.DownloadData(url);
                    using(var ms = new MemoryStream(data))
                    {
                        using (var image = Image.FromStream(ms))
                        {
                            image.Save(fullFilePath, ImageFormat.Jpeg);
                        }
                    }
                }
                return fullFilePath;
            }

            return "";
        }

        private string GetUniqueFilename()
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

        public bool DoesLocationExist(string location)
        {
            var task = Task.Run(() => _client.GetAsync(_baseUrl + "/geocoding/v1/address?key=" + _apiKey + "&location=" + location));
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
