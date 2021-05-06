using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    class MainViewModel: ViewModelBase
    {
        private ITourFactory _tourFactory;
        private Tour _currentTour;
        private string _searchTourName;

        private ICommand _searchCommand;
        private ICommand _addCommand;
        private ICommand _removeCommand;
        private ICommand _addLogCommand;
        private ICommand _removeLogCommand;



        public ICommand SearchCommand => _searchCommand ??= new RelayCommand(Search);
        public ICommand AddCommand => _addCommand ??= new RelayCommand(Add);
        public ICommand RemoveCommand => _removeCommand ??= new RelayCommand(Remove);
        public ICommand AddLogCommand => _addLogCommand ??= new RelayCommand(AddLog);
        public ICommand RemoveLogCommand => _removeLogCommand ??= new RelayCommand(RemoveLog);

        public ObservableCollection<Tour> Tours { get; set; }

        public Tour CurrentTour
        {
            get => _currentTour;
            set{
                if (_currentTour != value && value != null) {
                    _currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                }
            }
        }

        public String SearchTourName
        {
            get => _searchTourName;
            set{
                if (_searchTourName != value) {
                    _searchTourName = value;
                    RaisePropertyChangedEvent(nameof(SearchTourName));
                }
            }
        }

        public MainViewModel()
        {
            this._tourFactory = TourFactory.GetInstance();
            InitListBox();
        }

        private void InitListBox()
        {
            Tours = new ObservableCollection<Tour>();
            FillListBox();
        }

        private void FillListBox()
        {
            foreach (var tour in this._tourFactory.GetTours())
            {
                Tours.Add(tour);
            }
        }

        private void Search(object commandParameter)
        {
            IEnumerable foundTours = this._tourFactory.Search(SearchTourName);
            Tours.Clear();
            foreach (Tour tour in foundTours)
            {
                Tours.Add(tour);
            }
        }
        private void Add(object commandParameter)
        {
            SearchTourName = "";
            Search(null);
            AddTourWindow addTourWindow = new AddTourWindow();
            addTourWindow.Show();
        }
        private void Remove(object commandParameter)
        {
            throw new System.NotImplementedException();
        }
        private void AddLog(object commandParameter)
        {
            throw new System.NotImplementedException();
        }
        private void RemoveLog(object commandParameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
