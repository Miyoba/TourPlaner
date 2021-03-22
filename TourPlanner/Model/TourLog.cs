using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace TourPlanner.Model
{
    public class TourLog: INotifyPropertyChanged
    {
        private string _time;
        private string _report;
        private int _distance;
        private string _totalTime;
        private double _rating;
        public TourLog(string time, string report, int distance, string totalTime, double rating)
        {
            _time = time;
            _report = report;
            _distance = distance;
            _totalTime = totalTime;
            _rating = rating;
        }

        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(); // using CallerMemberName
            }
        }

        public string Report
        {
            get => _report;
            set
            {
                _report = value;
                OnPropertyChanged();
            }
        }

        public int Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChanged();
            }
        }

        public string TotalTime
        {
            get => _totalTime;
            set
            {
                _totalTime = value;
                OnPropertyChanged();
            }
        }

        public double Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}