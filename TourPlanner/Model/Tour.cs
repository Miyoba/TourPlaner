using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TourPlanner.Model
{
    class Tour : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private string _routeInformation;
        private int _distance;
        public Tour(string name, string description, string routeInformation, int distance)
        {
            _name = name;
            _description = description;
            _routeInformation = routeInformation;
            _distance = distance;
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(); // using CallerMemberName
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string RouteInformation
        {
            get => _routeInformation;
            set
            {
                _routeInformation = value;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
