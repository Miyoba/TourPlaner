using System;
using Microsoft.Win32;

namespace TourPlanner.BusinessLayer {
    class MySaveFileDialog : ISaveFileDialog {

        private SaveFileDialog _saveFileDialog;

        public string Filter { 
            get => _saveFileDialog.Filter;
            set => _saveFileDialog.Filter = value;
        }

        public string FileName
        {
            get => _saveFileDialog.FileName;
            set => _saveFileDialog.FileName = value;
        }

        public MySaveFileDialog()
        {
            _saveFileDialog = new SaveFileDialog();
        }
        public bool? ShowDialog()
        {
            return _saveFileDialog.ShowDialog();
        }

        
    }
}
