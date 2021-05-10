using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;

namespace TourPlanner.ViewModels {
    class EditTourViewModel : ViewModelBase
    {

        private Window _window;

        private string _tourName;
        private string _tourDescription;
        private string _tourRouteInformation;
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

        public String TourRouteInformation
        {
            get => _tourRouteInformation;
            set{
                if (_tourRouteInformation != value) {
                    _tourRouteInformation = value;
                    RaisePropertyChangedEvent(nameof(TourRouteInformation));
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

        public EditTourViewModel(Window window, Tour tour)
        {
            _window = window;
            _tourName = tour.Name;
            _tourDescription = tour.Description;
            _tourRouteInformation = tour.RouteInformation;
            _tourDistance = tour.Distance;

        }

        private void EditTour(object commandParameter)
        {
            //TODO Edit Tour Information to DAO
            _window.Close();
        }

        private void CancelTour(object commandParameter)
        {
            _window.Close();
        }
    }
}
