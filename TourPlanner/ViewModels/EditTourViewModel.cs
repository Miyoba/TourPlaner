using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Logger;
using TourPlanner.Models;

namespace TourPlanner.ViewModels {
    class EditTourViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        private Window _window;
        private MainViewModel _mainView;
        private ITourPlannerFactory _tourPlannerFactory;

        private Tour _tour;

        private string _tourName;
        private string _tourDescription;
        private string _tourFromLocation;
        private string _tourToLocation;
        private int _tourDistance;

        private ICommand _editTourCommand;
        private ICommand _cancelTourCommand;


        public ICommand EditTourCommand => _editTourCommand ??= new RelayCommand(EditTour);
        public ICommand CancelTourCommand => _cancelTourCommand ??= new RelayCommand(CancelTour);

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

        public String TourDescription
        {
            get => _tourDescription;
            set{
                if (_tourDescription != value) {
                    _tourDescription = value;
                    RaisePropertyChangedEvent(nameof(TourDescription));
                }
            }
        }

        public String TourFromLocation
        {
            get => _tourFromLocation;
            set{
                if (_tourFromLocation != value) {
                    _tourFromLocation = value;
                    RaisePropertyChangedEvent(nameof(TourFromLocation));
                }
            }
        }

        public String TourToLocation
        {
            get => _tourToLocation;
            set{
                if (_tourToLocation != value) {
                    _tourToLocation = value;
                    RaisePropertyChangedEvent(nameof(TourToLocation));
                }
            }
        }

        public int TourDistance
        {
            get => _tourDistance;
            set{
                if (_tourDistance != value) {
                    _tourDistance = value;
                    RaisePropertyChangedEvent(nameof(TourDistance));
                }
            }
        }

        public EditTourViewModel(Window window, Tour tour, MainViewModel mainView)
        {
            _log.Debug("Initializing Edit Tour Window.");
            _window = window;
            _mainView = mainView;
            _tour = tour;
            _tourName = tour.Name;
            _tourDescription = tour.Description;
            _tourFromLocation = tour.FromLocation;
            _tourToLocation = tour.ToLocation;
            _tourDistance = tour.Distance;

            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
        }

        private void EditTour(object commandParameter)
        {
            _log.Info("Edit Tour function is going to be executed.");
            Tour tour = _tourPlannerFactory.EditTour(_tour, TourName, TourDescription, TourFromLocation, TourToLocation,
                TourDistance);
            if (tour != null)
            {
                _mainView.Tours.Remove(_tour);
                _mainView.Tours.Add(tour);
            }
            _window.Close();
        }

        private void CancelTour(object commandParameter)
        {
            _log.Info("Edit Tour process was canceled.");
            _window.Close();
        }
    }
}
