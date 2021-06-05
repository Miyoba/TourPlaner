using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Logger;
using TourPlanner.Models;

namespace TourPlanner.ViewModels {
    class AddLogViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        private Window _window;
        private MainViewModel _mainView;
        private ITourPlannerFactory _tourPlannerFactory;

        private Tour _tour;

        private string _tourName;

        private string _dateTime;
        private string _report;
        private int _distance;
        private string _totalTime;
        private int _rating;
        private string _vehicle;
        private int _avgSpeed;
        private string _people;
        private int _breaks;
        private int _linearDistance;

        private ICommand _addLogCommand;
        private ICommand _cancelLogCommand;
        

        public ICommand AddLogCommand => _addLogCommand ??= new RelayCommand(AddLog);
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
        public string Vehicle
        {
            get => _vehicle;
            set{
                if (_vehicle != value) {
                    _vehicle = value;
                    RaisePropertyChangedEvent(nameof(Vehicle));
                }
            }
        }
        public int AvgSpeed
        {
            get => _avgSpeed;
            set{
                if (_avgSpeed != value) {
                    _avgSpeed = value;
                    RaisePropertyChangedEvent(nameof(AvgSpeed));
                }
            }
        }
        public string People
        {
            get => _people;
            set{
                if (_people != value) {
                    _people = value;
                    RaisePropertyChangedEvent(nameof(People));
                }
            }
        }
        public int Breaks
        {
            get => _breaks;
            set{
                if (_breaks != value) {
                    _breaks = value;
                    RaisePropertyChangedEvent(nameof(Breaks));
                }
            }
        }
        public int LinearDistance
        {
            get => _linearDistance;
            set{
                if (_linearDistance != value) {
                    _linearDistance = value;
                    RaisePropertyChangedEvent(nameof(LinearDistance));
                }
            }
        }

        public AddLogViewModel(Window window, Tour tour, MainViewModel mainView)
        {
            _log.Debug("Initializing Add Log Window.");
            _window = window;
            _tourName = tour.Name;
            _tour = tour;
            _mainView = mainView;
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
        }

        private void AddLog(object commandParameter)
        {
            _log.Info("Add Log function is going to be executed.");
            TourLog tourLog = _tourPlannerFactory.AddTourLog(_tour, _dateTime, _report,_distance, _totalTime, _rating, _vehicle, _avgSpeed, _people, _breaks, _linearDistance);
            _mainView.Logs.Add(tourLog);
            _window.Close();
        }

        private void CancelLog(object commandParameter)
        {
            _log.Info("Add Log process was canceled.");
            _window.Close();
        }
    }
}
