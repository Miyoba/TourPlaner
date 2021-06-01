using System.Collections.Generic;
using System.Linq;
using TourPlanner.DataAccessLayer.Common;
using TourPlanner.DataAccessLayer.DAO;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    internal class TourPlannerFactoryImpl : ITourPlannerFactory
    {

        public IEnumerable<Tour> GetTours()
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            return tourDao.GetTours();
        }

        public IEnumerable<TourLog> GetTourLogs(Tour tour)
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.GetTourLogs(tour);
        }

        public IEnumerable<Tour> FindTour(IEnumerable<Tour> tours, IEnumerable<Tour> found, string fieldName, string searchArg, bool caseSensitive = false)
        {
            searchArg = caseSensitive ? searchArg : searchArg.ToLower();
            return found.Concat(tours.Where(x => x.GetFieldValue(fieldName, caseSensitive).Contains(searchArg)));
        }

        public IEnumerable<TourLog> FindTourLog(IEnumerable<TourLog> tourLogs, IEnumerable<TourLog> found, string fieldName, string searchArg, bool caseSensitive = false)
        {
            searchArg = caseSensitive ? searchArg : searchArg.ToLower();
            return found.Concat(tourLogs.Where(x => x.GetFieldValue(fieldName, caseSensitive).Contains(searchArg)));
        }

        public IEnumerable<Tour> Search(string searchArg, bool caseSensitive = false) {
            IEnumerable<Tour> tours = GetTours();
            IEnumerable<Tour> found = new List<Tour>();

            if (searchArg != null)
            {
                var enumerable = tours.ToList();
                found = FindTour(enumerable, found, "Name", searchArg, caseSensitive);
                found = FindTour(enumerable, found, "FromLocation", searchArg, caseSensitive);
                found = FindTour(enumerable, found, "ToLocation", searchArg, caseSensitive);
                found = FindTour(enumerable, found, "Description", searchArg, caseSensitive);
                found = FindTour(enumerable, found, "Distance", searchArg, caseSensitive);
            }

            return found.Distinct();

        }

        public IEnumerable<TourLog> SearchTourLog(Tour tour, string searchArg, bool caseSensitive = false)
        {
            IEnumerable<TourLog> tourLogs = GetTourLogs(tour);
            IEnumerable<TourLog> found = new List<TourLog>();

            if (searchArg != null)
            {
                var enumerable = tourLogs.ToList();
                found = FindTourLog(enumerable, found, "DateTime", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "Report", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "Distance", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "TotalTime", searchArg, caseSensitive);
                found = FindTourLog(enumerable, found, "Rating", searchArg, caseSensitive);
            }

            return found.Distinct();
            }

        public Tour AddTour(string tourName, string tourDescription, string tourFromLocation, string tourToLocation, int tourDistance)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            IMapQuest mapQuest = new MapQuest();
            string imagePath = mapQuest.LoadImage(tourFromLocation, tourToLocation);
            return tourDao.AddNewTour(tourName, tourFromLocation, tourToLocation, tourDescription, tourDistance, imagePath);
        }

        public TourLog AddTourLog(Tour tour, string dateTime, string report, int distance, string totalTime, int rating)
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.AddNewTourLog(tour, dateTime, report, distance, totalTime, rating);
        }

        public void DeleteTour(Tour tour)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            if (tour.HasImage())
            {
                //TODO Fix image deletion
                //Image tempImage = Image.FromFile(tour.ImagePath);
                //tempImage.Dispose();
                //File.Delete(tour.ImagePath);
            }

            tourDao.DeleteTour(tour);
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            tourLogDao.DeleteTourLog(tourLog);
        }

        public Tour EditTour(Tour tour, string tourName, string tourDescription, string tourFromLocation, string tourToLocation,
            int tourDistance)
        {
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            IMapQuest mapQuest = new MapQuest();
            string imagePath = mapQuest.LoadImage(tourFromLocation, tourToLocation);
            return tourDao.EditTour(tour, tourName, tourDescription, tourFromLocation, tourToLocation, tourDistance, imagePath);
        }

        public TourLog EditTourLog(TourLog tourLog, string dateTime, string report, int distance, string totalTime, int rating)
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return tourLogDao.EditTourLog(tourLog, dateTime, report, distance, totalTime, rating);
        }

        public bool ExportData()
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            IDataFileManager manager = new DataFileManager();

            IEnumerable<TourLog> logs = tourLogDao.GetAllLogs();

            return manager.ExportData(GetTours(), logs);
        }

        public bool ImportData()
        {
            IDataFileManager manager = new DataFileManager();
            JsonData data = manager.ImportData();

            if (data == null)
                return false;

            foreach (var tour in data.Tours)
            {
                Tour dbTour = AddTour(tour.Name, tour.Description, tour.FromLocation, tour.ToLocation, tour.Distance);
                foreach (var log in data.Logs)
                {
                    if (tour.Id == log.TourId)
                    {
                        AddTourLog(dbTour, log.DateTime, log.Report, log.Distance, log.TotalTime, log.Rating);
                    }
                }
            }
            return true;
        }

        public bool PrintData(Tour currentTour)
        {
            ReportGenerator gen = new ReportGenerator();
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return gen.GeneratePDFReportForTours(new List<Tour>() {currentTour}, tourLogDao.GetTourLogs(currentTour), false);
        }

        public bool PrintAllData()
        {
            ReportGenerator gen = new ReportGenerator();
            ITourDAO tourDao = DALFactory.CreateTourDAO();
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            return gen.GeneratePDFReportForTours(tourDao.GetTours(), tourLogDao.GetAllLogs(), true);
        }
    }
}
