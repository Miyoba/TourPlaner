using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TourPlanner.Model;

namespace TourPlanner
{
    class MainViewModel: INotifyPropertyChanged
    {

        public ObservableCollection<Tour> Data { get; }
            = new ObservableCollection<Tour>();

        public string Name { get; set; }
        public string Description { get; set; }
        public string RouteInformation { get; set; }
        public int Distance { get; set; }
        public RelayCommand AddCommand { get; }

        public MainViewModel()
        {
            AddCommand = new RelayCommand((_) =>
            {
                Data.Add(new Tour(this.Name, this.Description, this.RouteInformation, this.Distance));
                Name = String.Empty;
                Description = String.Empty;
                RouteInformation = String.Empty;
                Distance = 0;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(RouteInformation));
                OnPropertyChanged(nameof(Distance));
            });

            // real data to add (not design data)
            Data.Add(new Tour("TestTour","TestTourDescription","TestTourInfo", 0));
            Data.Add(new Tour("TestTour2","TestTourDescription2","TestTourInfo2", 2));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
