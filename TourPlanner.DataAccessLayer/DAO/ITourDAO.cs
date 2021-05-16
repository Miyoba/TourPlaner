using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.DAO {
    public interface ITourDAO
    {
        Tour FindById(int tourId);
        Tour AddNewTour(string tourName, string tourFromLocation, string tourToLocation, string tourDescription, int tourDistance);
        IEnumerable<Tour> GetTours();
    }
}
