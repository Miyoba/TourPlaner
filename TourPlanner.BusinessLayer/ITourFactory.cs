using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactory
    {
        IEnumerable<Tour> GetTours();
        IEnumerable<TourLog> GetTourLogs(Tour tour);
        IEnumerable<Tour> Search(string searchArg, bool caseSensitive = false);
        IEnumerable<TourLog> SearchTourLog(Tour tour, string searchArg, bool caseSensitive = false);
        bool AddTour(string tourName, string tourDescription, string tourRouteInformation, int tourDistance);
    }
}
