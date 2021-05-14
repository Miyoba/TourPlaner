using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;

namespace TourPlanner.ViewModels {
    class EditLogViewModel : ViewModelBase
    {

        private Window _window;

        private string _tourName;

        private string _dateTime;
        private string _report;
        private int _distance;
        private string _totalTime;
        private int _rating;

        private ICommand _editLogCommand;
        private ICommand _cancelLogCommand;

        public ICommand EditLogCommand => _editLogCommand ??= new RelayCommand(EditLog);
        public ICommand CancelLogCommand => _cancelLogCommand ??= new RelayCommand(CancelLog);

        public String TourName
        {
            get => _tourName;
            set{
                if (_tourName != value) {
                    _tourName = value;
                    RaisePropertyChangedEvent(nameof(TourName));
                }
            }
        }

        public String DateTime
        {
            get => _dateTime;
            set{
                if (_dateTime != value) {
                    _dateTime = value;
                    RaisePropertyChangedEvent(nameof(DateTime));
                }
            }
        }

        public String Report
        {
            get => _report;
            set{
                if (_report != value) {
                    _report = value;
                    RaisePropertyChangedEvent(nameof(Report));
                }
            }
        }

        public int Distance
        {
            get => _distance;
            set{
                if (_distance != value) {
                    _distance = value;
                    RaisePropertyChangedEvent(nameof(Distance));
                }
            }
        }

        public String TotalTime
        {
            get => _totalTime;
            set{
                if (_totalTime != value) {
                    _totalTime = value;
                    RaisePropertyChangedEvent(nameof(TotalTime));
                }
            }
        }
        public int Rating
        {
            get => _rating;
            set{
                if (_rating != value) {
                    _rating = value;
                    RaisePropertyChangedEvent(nameof(Rating));
                }
            }
        }

        public EditLogViewModel(Window window, Tour tour, TourLog log)
        {
            _window = window;
            _tourName = tour.Name;
            _dateTime = log.DateTime;
            _report = log.Report;
            _distance = log.Distance;
            _totalTime = log.TotalTime;
            _rating = log.Rating;
        }

        private void EditLog(object commandParameter)
        {
            //TODO Add Tour Information to DAO
            _window.Close();
        }

        private void CancelLog(object commandParameter)
        {
            _window.Close();
        }
    }
}
