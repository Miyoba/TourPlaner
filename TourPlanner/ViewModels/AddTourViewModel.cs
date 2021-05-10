using System;
using System.Windows;
using System.Windows.Input;

namespace TourPlanner.ViewModels {
    class AddTourViewModel : ViewModelBase
    {

        private Window _window;

        private string _tourName;
        private string _tourDescription;
        private string _tourRouteInformation;
        private int _tourDistance;

        private ICommand _addTourCommand;
        private ICommand _cancelTourCommand;

        public ICommand AddTourCommand => _addTourCommand ??= new RelayCommand(AddTour);
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

        public AddTourViewModel(Window window)
        {
            _window = window;
        }

        private void AddTour(object commandParameter)
        {
            //TODO Add Tour Information to DAO
            _window.Close();
        }

        private void CancelTour(object commandParameter)
        {
            _window.Close();
        }
    }
}
