﻿using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;

namespace TourPlanner.ViewModels {
    class AddTourViewModel : ViewModelBase
    {

        private Window _window;
        private ITourPlannerFactory _tourPlannerFactory;

        private string _tourName;
        private string _tourDescription;
        private string _tourFromLocation;
        private string _tourToLocation;
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

        public AddTourViewModel(Window window)
        {
            _window = window;
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
        }

        private void AddTour(object commandParameter)
        {
            _tourPlannerFactory.AddTour(_tourName, _tourDescription, _tourFromLocation, _tourToLocation, _tourDistance);
            _window.Close();
        }

        private void CancelTour(object commandParameter)
        {
            _window.Close();
        }
    }
}
