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
                new Tour() {Name = "TestTour"},
                new Tour() {Name = "TestTour2"},
                new Tour() {Name = "Wien"},
                new Tour() {Name = "Paris"},
                new Tour() {Name = "Berlin"}
            };
        }
    }
}