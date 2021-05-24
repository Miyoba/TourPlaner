using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using TourPlanner.BusinessLayer;
using TourPlanner.Logger;
using TourPlanner.Models;
using TourPlanner.Windows;

namespace TourPlanner.ViewModels {
    public class MainViewModel : ViewModelBase {
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
                if (_currentTour != value) {
                    _currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));

                   FillTourLogListBox(_currentTour);
                }
                else
                    _log.Debug("Selected Tour did not change");
            }
        }

        public TourLog CurrentLog
        {
            get => _currentLog;
            set
            {
                if (_currentLog != value)
                {
                    _currentLog = value;
                    RaisePropertyChangedEvent(nameof(CurrentLog));
                }
                else
                    _log.Debug("Selected Log did not change");
            }
        }

        public String SearchTourName
        {
            get
            {
                if (_searchTourName == null)
                    return "";
                return _searchTourName;
            }
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
            get
            {
                if (_searchLogValue == null)
                    return "";
                return _searchLogValue;
            }
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
            FillTourListBox();
        }

        private void FillTourListBox()
        {
            _log.Debug("Filling Tour Collections.");
            Tours.Clear();
            foreach (var tour in this._tourPlannerFactory.GetTours())
            {
                Tours.Add(tour);
            }
        }

        private void FillTourLogListBox(Tour tour)
        {
            _log.Debug("Filling Log Collections.");
            if (tour != null)
            {
                Logs.Clear();
                foreach (var log in this._tourPlannerFactory.GetTourLogs(tour))
                {
                    Logs.Add(log);
                }
            }
            else
                _log.Debug("Can't fill Logs. No Tour was selected.");

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
            if (CurrentTour != null)
            {
                IEnumerable foundTourLogs = this._tourPlannerFactory.SearchTourLog(CurrentTour, SearchLogValue);
                Logs.Clear();
                foreach (TourLog tourLog in foundTourLogs)
                {
                    Logs.Add(tourLog);
                }
            }
            else
                _log.Debug("Can't search Logs. No Tour was selected.");
        }
        private void Add(object commandParameter)
        {
            _log.Info("Add Tour function was called. Opening AddTourWindow.");
            SearchTourName = null;
            Search(null);
            AddTourWindow addTourWindow = new AddTourWindow(this);
            addTourWindow.Show();
        }
        private void Edit(object commandParameter)
        {
            _log.Info("Edit Tour function was called.");
            if (CurrentTour != null)
            {
                EditTourWindow editTourWindow = new EditTourWindow(this, CurrentTour);
                editTourWindow.Show();
            }
            else
                _log.Debug("Can't edit Tour. No Tour was selected.");
        }
        private void Remove(object commandParameter)
        {
            _log.Info("Remove Tour function was called.");
            if (CurrentTour != null)
            {
                _tourPlannerFactory.DeleteTour(CurrentTour);
                CurrentTour = null;
                Logs.Clear();
                FillTourListBox();
            }
            else
                _log.Debug("Can't remove Tour. No Tour was selected.");
        }
        private void AddLog(object commandParameter)
        {
            _log.Info("Add Log function was called.");
            if (CurrentTour != null)
            {
                SearchLogValue = null;
                SearchLog(null);
                AddLogWindow addLogWindow = new AddLogWindow(this, CurrentTour);
                addLogWindow.Show();
            }
            else
                _log.Debug("Can't add Log. No corresponding Tour was selected.");
        }
        private void EditLog(object commandParameter)
        {
            _log.Info("Edit Log function was called.");
            if (CurrentLog != null)
            {
                EditLogWindow editLogWindow = new EditLogWindow(this, CurrentTour, CurrentLog);
                editLogWindow.Show();
            }
            else
                _log.Debug("Can't edit Log. No Log was selected.");
        }
        private void RemoveLog(object commandParameter)
        {
            _log.Info("Remove TourLog function was called.");
            if (CurrentLog != null)
            {
                _tourPlannerFactory.DeleteTourLog(CurrentLog);
                CurrentLog = null;
                FillTourLogListBox(CurrentTour);
            }
            else
                _log.Debug("Can't remove Tour. No Tour was selected.");
        }
        private void ImportData(object commandParameter)
        {
            _log.Info("Import data function was called.");
            if (_tourPlannerFactory.ImportData())
            {
                _log.Info("Data was successfully exported.");
                FillTourListBox();
                MessageBox.Show("Data successfully imported!", "Import Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _log.Warn("Unsuccessfully tried to export data.");
                var dialog = MessageBox.Show("An unexpected error occurred while importing the data!", "Import Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ExportData(object commandParameter)
        {
            _log.Info("Export data function was called.");
            if (_tourPlannerFactory.ExportData())
            {
                _log.Info("Data was successfully exported.");
                MessageBox.Show("Data successfully exported!", "Export Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _log.Warn("Unsuccessfully tried to export data.");
                var dialog = MessageBox.Show("An unexpected error occurred while exporting the data!", "Export Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void PrintData(object commandParameter)
        {
            _log.Info("Print data function was called.");
            throw new System.NotImplementedException();
        }
    }
}
