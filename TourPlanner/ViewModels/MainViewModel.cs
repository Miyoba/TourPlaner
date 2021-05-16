using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;
using TourPlanner.Windows;

namespace TourPlanner.ViewModels
{
    class MainViewModel: ViewModelBase
    {
        private ITourPlannerFactory _tourPlannerFactory;
        private Tour _currentTour;
        private TourLog _currentLog;

        private string _searchTourName;
        private string _searchLogValue;

        private ICommand _searchCommand;
        private ICommand _searchLogCommand;
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _removeCommand;
        private ICommand _addLogCommand;
        private ICommand _editLogCommand;
        private ICommand _removeLogCommand;
        private ICommand _importDataCommand;
        private ICommand _exportDataCommand;
        private ICommand _printDataCommand;




        public ICommand SearchCommand => _searchCommand ??= new RelayCommand(Search);
        public ICommand SearchLogCommand => _searchLogCommand ??= new RelayCommand(SearchLog);
        public ICommand AddCommand => _addCommand ??= new RelayCommand(Add);
        public ICommand EditCommand => _editCommand ??= new RelayCommand(Edit);
        public ICommand RemoveCommand => _removeCommand ??= new RelayCommand(Remove);
        public ICommand AddLogCommand => _addLogCommand ??= new RelayCommand(AddLog);
        public ICommand EditLogCommand => _editLogCommand ??= new RelayCommand(EditLog);
        public ICommand RemoveLogCommand => _removeLogCommand ??= new RelayCommand(RemoveLog);
        public ICommand ImportDataCommand => _importDataCommand ??= new RelayCommand(ImportData);
        public ICommand ExportDataCommand => _exportDataCommand ??= new RelayCommand(ExportData);
        public ICommand PrintDataCommand => _printDataCommand ??= new RelayCommand(PrintData);

        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<TourLog> Logs { get; set; }

        public Tour CurrentTour
        {
            get => _currentTour;
            set{
                if (_currentTour != value && value != null) {
                    _currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));

                    Logs.Clear();
                    foreach (var log in this._tourPlannerFactory.GetTourLogs(_currentTour))
                    {
                        Logs.Add(log);
                    }
                }
            }
        }

        public TourLog CurrentLog
        {
            get => _currentLog;
            set{
                if (_currentLog != value && value != null) {
                    _currentLog = value;
                    RaisePropertyChangedEvent(nameof(CurrentLog));
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

        public String SearchLogValue
        {
            get => _searchLogValue;
            set{
                if (_searchLogValue != value) {
                    _searchLogValue = value;
                    RaisePropertyChangedEvent(nameof(SearchTourName));
                }
            }
        }

        public MainViewModel()
        {
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
            InitListBox();
        }

        private void InitListBox()
        {
            Tours = new ObservableCollection<Tour>();
            Logs = new ObservableCollection<TourLog>();
            FillListBox();
        }

        private void FillListBox()
        {
            foreach (var tour in this._tourPlannerFactory.GetTours())
            {
                Tours.Add(tour);
            }
        }

        private void Search(object commandParameter)
        {
            IEnumerable foundTours = this._tourPlannerFactory.Search(SearchTourName);
            Tours.Clear();
            foreach (Tour tour in foundTours)
            {
                Tours.Add(tour);
            }
        }

        private void SearchLog(object commandParameter)
        {
            IEnumerable foundTourLogs = this._tourPlannerFactory.SearchTourLog(CurrentTour, SearchLogValue);
            Logs.Clear();
            foreach (TourLog tourLog in foundTourLogs)
            {
                Logs.Add(tourLog);
            }
        }
        private void Add(object commandParameter)
        {
            SearchTourName = "";
            Search(null);
            AddTourWindow addTourWindow = new AddTourWindow();
            addTourWindow.Show();
        }
        private void Edit(object commandParameter)
        {
            if (_currentTour != null)
            {
                EditTourWindow editTourWindow = new EditTourWindow(_currentTour);
                editTourWindow.Show();
            }
        }
        private void Remove(object commandParameter)
        {
            throw new System.NotImplementedException();
        }
        private void AddLog(object commandParameter)
        {
            if (_currentTour != null)
            {
                AddLogWindow addLogWindow = new AddLogWindow(_currentTour);
                addLogWindow.Show();
            }
        }
        private void EditLog(object commandParameter)
        {
            if (_currentLog != null)
            {
                EditLogWindow editLogWindow = new EditLogWindow(_currentTour, _currentLog);
                editLogWindow.Show();
            }
        }
        private void RemoveLog(object commandParameter)
        {
            throw new System.NotImplementedException();
        }
        private void ImportData(object commandParameter)
        {
            throw new System.NotImplementedException();
        }
        private void ExportData(object commandParameter)
        {
            throw new System.NotImplementedException();
        }
        private void PrintData(object commandParameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
