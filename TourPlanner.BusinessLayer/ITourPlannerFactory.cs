using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface ITourPlannerFactory
    {
        IEnumerable<Tour> GetTours();
        IEnumerable<TourLog> GetTourLogs(Tour tour);
        IEnumerable<Tour> Search(string searchArg, bool caseSensitive = false);
        IEnumerable<TourLog> SearchTourLog(Tour tour, string searchArg, bool caseSensitive = false);
        void AddTour(string tourName, string tourDescription, string tourFromLocation, string tourToLocation, int tourDistance);
        void AddTourLog(Tour tour, string dateTime, string report, int distance, string totalTime, int rating);
        void DeleteTour(Tour tour);
        void DeleteTourLog(TourLog tourLog);
        void EditTour(Tour tour, string tourName, string tourDescription, string tourFromLocation, string tourToLocation, int tourDistance);
        void EditTourLog(TourLog tourLog, string dateTime, string report, int distance, string totalTime, int rating);
    }
}
