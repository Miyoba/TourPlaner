using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Logger;
using TourPlanner.Models;
using TourPlanner.Windows;

namespace TourPlanner.ViewModels {
    class MainViewModel : ViewModelBase {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

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
            set
            {
                if (_currentTour != value && value != null) {
                    _currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));

                    Logs.Clear();
                    foreach (var log in this._tourPlannerFactory.GetTourLogs(_currentTour))
                    {
                        Logs.Add(log);
                    }
                }
                else
                    _log.Debug("Selected Tour did not change or was null");
            }
        }

        public TourLog CurrentLog
        {
            get => _currentLog;
            set
            {
                if (_currentLog != value && value != null)
                {
                    _currentLog = value;
                    RaisePropertyChangedEvent(nameof(CurrentLog));
                }
                else
                    _log.Debug("Selected Log did not change or was null");
            }
        }

        public String SearchTourName
        {
            get => _searchTourName;
            set
            {
                if (_searchTourName != value)
                {
                    _searchTourName = value;
                    RaisePropertyChangedEvent(nameof(SearchTourName));
                }
            }
        }

        public String SearchLogValue
        {
            get => _searchLogValue;
            set
            {
                if (_searchLogValue != value)
                {
                    _searchLogValue = value;
                    RaisePropertyChangedEvent(nameof(SearchLogValue));
                }
            }
        }

        public MainViewModel()
        {
            _log.Debug("Initializing Main Window.");
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
            InitListBox();
        }

        private void InitListBox()
        {
            _log.Debug("Declaring Tour and Log Collections.");
            Tours = new ObservableCollection<Tour>();
            Logs = new ObservableCollection<TourLog>();
            FillListBox();
        }

        private void FillListBox()
        {
            _log.Debug("Initializing Tour and Log Collections.");
            foreach (var tour in this._tourPlannerFactory.GetTours())
            {
                Tours.Add(tour);
            }
        }

        private void Search(object commandParameter)
        {
            _log.Info("Search Tour function was called.");
            IEnumerable foundTours = this._tourPlannerFactory.Search(SearchTourName);
            Tours.Clear();
            foreach (Tour tour in foundTours)
            {
                Tours.Add(tour);
            }
        }

        private void SearchLog(object commandParameter)
        {
            _log.Info("Search Log function was called.");
            IEnumerable foundTourLogs = this._tourPlannerFactory.SearchTourLog(CurrentTour, SearchLogValue);
            Logs.Clear();
            foreach (TourLog tourLog in foundTourLogs)
            {
                Logs.Add(tourLog);
            }
        }
        private void Add(object commandParameter)
        {
            _log.Info("Add Tour function was called.");
            SearchTourName = "";
            Search(null);
            AddTourWindow addTourWindow = new AddTourWindow();
            addTourWindow.Show();
        }
        private void Edit(object commandParameter)
        {
            _log.Info("Edit Tour function was called.");
            if (_currentTour != null)
            {
                EditTourWindow editTourWindow = new EditTourWindow(_currentTour);
                editTourWindow.Show();
            }
            else
            {
                _log.Debug("Can't edit Tour. No Tour was selected.");
            }
        }
        private void Remove(object commandParameter)
        {
            _log.Info("Remove Tour function was called.");
            throw new System.NotImplementedException();
        }
        private void AddLog(object commandParameter)
        {
            _log.Info("Add Log function was called.");
            if (_currentTour != null)
            {
                SearchLogValue = "";
                SearchLog(null);
                AddLogWindow addLogWindow = new AddLogWindow(_currentTour);
                addLogWindow.Show();
            }
            else
            {
                _log.Debug("Can't add Log. No corresponding Tour was selected.");
            }
        }
        private void EditLog(object commandParameter)
        {
            _log.Info("Edit Log function was called.");
            if (_currentLog != null)
            {
                EditLogWindow editLogWindow = new EditLogWindow(_currentTour, _currentLog);
                editLogWindow.Show();
            }
            else
            {
                _log.Debug("Can't edit Log. No Log was selected.");
            }
        }
        private void RemoveLog(object commandParameter)
        {
            _log.Info("Remove Log function was called.");
            throw new System.NotImplementedException();
        }
        private void ImportData(object commandParameter)
        {
            _log.Info("Import data function was called.");
            throw new System.NotImplementedException();
        }
        private void ExportData(object commandParameter)
        {
            _log.Info("Export data function was called.");
            throw new System.NotImplementedException();
        }
        private void PrintData(object commandParameter)
        {
            _log.Info("Print data function was called.");
            throw new System.NotImplementedException();
        }
    }
}
