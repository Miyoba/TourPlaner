using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer {
    class DataFileManager {
        public static bool ExportData(IEnumerable<Tour> tours, IEnumerable<TourLog> logs)
        {
            SaveFileDialog sfdlg = new SaveFileDialog();  
            sfdlg.Filter = "Json Files (*.json) | *.json"; //Here you can filter which all files you wanted allow to open  
            sfdlg.ShowDialog();
            if (!sfdlg.FileName.Equals(""))
            {
                JsonData data = new JsonData(tours, logs);
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(sfdlg.FileName, json);
                return true;
            }
            return false;
        }

        public static JsonData ImportData()
        {
            OpenFileDialog ofd = new OpenFileDialog();  
            ofd.Title = "Open a Json File";  
            ofd.Filter = "Json Files (*.json) | *.json"; //Here you can filter which all files you wanted allow to open  
            ofd.ShowDialog();  
            if (!ofd.FileName.Equals("")) {  
                StreamReader sr = new StreamReader(ofd.FileName);
                string json = sr.ReadToEnd();  
                sr.Close();
                return JsonConvert.DeserializeObject<JsonData>(json);
            }
            return null;
        }
    }
}
