using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer {
    public class DataFileManager : IDataFileManager
    {

        private ISaveFileDialog _saveFileDialog;
        private IOpenFileDialog _openFileDialog;
        public DataFileManager(ISaveFileDialog sfd, IOpenFileDialog ofd)
        {
            _saveFileDialog = sfd;
            _openFileDialog = ofd;
        }
        public bool ExportData(IEnumerable<Tour> tours, IEnumerable<TourLog> logs)
        {
            _saveFileDialog.Filter = "Json Files (*.json) | *.json"; //Here you can filter which all files you wanted allow to open  
            _saveFileDialog.ShowDialog();
            if (!_saveFileDialog.FileName.Equals(""))
            {
                JsonData data = new JsonData(tours, logs);
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(_saveFileDialog.FileName, json);
                return true;
            }
            return false;
        }

        public JsonData ImportData()
        {
            _openFileDialog.Title = "Open a Json File";  
            _openFileDialog.Filter = "Json Files (*.json) | *.json"; //Here you can filter which all files you wanted allow to open  
            _openFileDialog.ShowDialog();  
            if (!_openFileDialog.FileName.Equals("")) {  
                StreamReader sr = new StreamReader(_openFileDialog.FileName);
                string json = sr.ReadToEnd();  
                sr.Close();
                return JsonConvert.DeserializeObject<JsonData>(json);
            }
            return null;
        }
    }
}
