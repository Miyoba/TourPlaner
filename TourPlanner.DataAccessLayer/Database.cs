using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer
{
    public class Database : IDataAccess
    {
        private string _connectionString;

        public Database()
        {
            // get from config
            _connectionString = " ... ";

            //establish connection
        }

        public List<Tour> GetTours()
        {
            return new List<Tour>()
            {
                new Tour() {Name = "TestTour", RouteInformation = "Route1", Distance = 33, Description = "Fun"},
                new Tour() {Name = "TestTour2", RouteInformation = "Route2", Distance = 98789, Description = "Really Long"},
                new Tour() {Name = "Wien", RouteInformation = "Wien - Paris", Distance = 5000, Description = "Le Baguette"},
                new Tour() {Name = "Paris", RouteInformation = "Paris - Berlin", Distance = 1524, Description = "Weiswurst"},
                new Tour() {Name = "Berlin", RouteInformation = "Berlin - Hamburg", Distance = 382, Description = "Kurz"}
            };
        }

        public List<TourLog> GetTourLogs(Tour tour)
        {
            return new List<TourLog>()
            {
                new TourLog() {Rating = 1},
                new TourLog() {Rating = 5},
                new TourLog() {Rating = 3}
            };
        }
    }
}
