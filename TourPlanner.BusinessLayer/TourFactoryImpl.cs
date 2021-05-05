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
            return _tourDAO.GetItems();
        }

        public IEnumerable<Tour> Search(string tourName, bool caseSensitive = false) {
            IEnumerable<Tour> tours = GetTours();

            if (caseSensitive)
            {
                return tours.Where(x => x.Name.Contains(tourName));
            }
            return tours.Where(x => x.Name.ToLower().Contains(tourName.ToLower()));
        }
    }
}
