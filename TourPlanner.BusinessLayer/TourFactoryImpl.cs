using System;
using System.Collections.Generic;
using System.Linq;
using TourPlanner.DataAccessLayer;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    internal class TourFactoryImpl : ITourFactory
    {
        private TourDAO _tourDAO = new TourDAO();
        public IEnumerable<Tour> GetTours()
        {
            return _tourDAO.GetTours();
        }

        public IEnumerable<TourLog> GetTourLogs(Tour tour)
        {
            return _tourDAO.GetTourLogs(tour);
        }

        public IEnumerable<Tour> Search(string searchArg, bool caseSensitive = false) {
            IEnumerable<Tour> tours = GetTours();
            IEnumerable<Tour> found = new List<Tour>();

            if (searchArg != null)
            {
                found = FindTour(tours, found, "Name", searchArg, caseSensitive);
                found = FindTour(tours, found, "RouteInformation", searchArg, caseSensitive);
                found = FindTour(tours, found, "Description", searchArg, caseSensitive);
                found = FindTour(tours, found, "Distance", searchArg, caseSensitive);
            }

            return found.Distinct();

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

        public IEnumerable<TourLog> SearchTourLog(Tour tour, string searchArg, bool caseSensitive = false)
        {
            IEnumerable<TourLog> tourLogs = GetTourLogs(tour);
            IEnumerable<TourLog> found = new List<TourLog>();
            if (searchArg != null)
            {
                found = FindTourLog(tourLogs, found, "DateTime", searchArg, caseSensitive);
                found = FindTourLog(tourLogs, found, "Report", searchArg, caseSensitive);
                found = FindTourLog(tourLogs, found, "Distance", searchArg, caseSensitive);
                found = FindTourLog(tourLogs, found, "TotalTime", searchArg, caseSensitive);
                found = FindTourLog(tourLogs, found, "Rating", searchArg, caseSensitive);
            }

            return found.Distinct();
            }

        public bool AddTour(string tourName, string tourDescription, string tourRouteInformation, int tourDistance)
        {
            throw new System.NotImplementedException();
        }
    }
}
