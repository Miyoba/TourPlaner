using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.DAO {
    public interface ITourDAO
    {
        Tour FindById(int tourId);
        void AddNewTour(string tourName, string tourFromLocation, string tourToLocation, string tourDescription, int tourDistance);
        void DeleteTour(Tour tour);
        void EditTour(Tour tour, string tourName, string tourFromLocation, string tourToLocation, string tourDescription, int tourDistance);
        IEnumerable<Tour> GetTours();
    }
}
