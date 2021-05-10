﻿using System.Collections.Generic;
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

        public IEnumerable<Tour> Search(string tourName, bool caseSensitive = false) {
            IEnumerable<Tour> tours = GetTours();

            if (caseSensitive)
            {
                //TODO Full text not only names
                return tours.Where(x => x.Name.Contains(tourName));
            }
            //TODO Full text not only names
            return tours.Where(x => x.Name.ToLower().Contains(tourName.ToLower()));
        }

        public bool AddTour(string tourName, string tourDescription, string tourRouteInformation, int tourDistance)
        {
            throw new System.NotImplementedException();
        }
    }
}
