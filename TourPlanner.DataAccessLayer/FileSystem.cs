using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer
{
    class FileSystem : IDataAccess
    {
        private string _filePath;

        public FileSystem()
        {
            //Get filepath from config
            _filePath = "...";
        }

        public List<Tour> GetTours()
        {
            //Get items from file system
            return new List<Tour>()
            {
                new Tour() {Name = "TestTour"},
                new Tour() {Name = "TestTour2"},
                new Tour() {Name = "Wien"},
                new Tour() {Name = "Paris"},
                new Tour() {Name = "Berlin"}
            };
        }

        public List<TourLog> GetTourLogs(Tour tour)
        {
            return new List<TourLog>()
            {
                new TourLog() {Rating = "Super"},
                new TourLog() {Rating = "Schlecht"},
                new TourLog() {Rating = "Naja"}
            };
        }
    }
}
