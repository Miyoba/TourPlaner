using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Logger;
using TourPlanner.Models;

namespace TourPlanner.ViewModels {
    class EditLogViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        private Window _window;
        private MainViewModel _mainView;
        private ITourPlannerFactory _tourPlannerFactory;

        private TourLog _tourLog;
        
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

        public EditLogViewModel(Window window, Tour tour, TourLog log, MainViewModel mainView)
        {
            _log.Debug("Initializing Edit Log Window.");
            _window = window;
            _mainView = mainView;
            _tourLog = log;
            _tourName = tour.Name;
            _dateTime = log.DateTime;
            _report = log.Report;
            _distance = log.Distance;
            _totalTime = log.TotalTime;
            _rating = log.Rating;

            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
        }

        private void EditLog(object commandParameter)
        {
            _log.Info("Edit Log function is going to be executed.");
            TourLog tourLog = _tourPlannerFactory.EditTourLog(_tourLog, DateTime, Report, Distance, TotalTime, Rating);
            if (tourLog != null)
            {
                _mainView.Logs.Remove(_tourLog);
                _mainView.Logs.Add(tourLog);
            }

            _window.Close();
        }

        private void CancelLog(object commandParameter)
        {
            _log.Info("Edit Log process was canceled.");
            _window.Close();
        }
    }
}
