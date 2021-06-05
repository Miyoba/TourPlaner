using Microsoft.Win32;

namespace TourPlanner.BusinessLayer {
    class MyOpenFileDialog : IOpenFileDialog
    {
        private OpenFileDialog _openFileDialog;

        public string Filter
        {
            get => _openFileDialog.Filter;
            set => _openFileDialog.Filter = value;
        }

        public string FileName
        {
            get => _openFileDialog.FileName;
            set => _openFileDialog.FileName = value;
        }

        public string Title
        {
            get => _openFileDialog.Title;
            set => _openFileDialog.Title = value;
        }

        public MyOpenFileDialog()
        {
            _openFileDialog = new OpenFileDialog();
        }

        public bool? ShowDialog()
        {
            return _openFileDialog.ShowDialog();
        }

        
    }
}
