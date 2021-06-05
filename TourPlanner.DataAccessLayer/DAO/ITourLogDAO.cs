using System;
using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.DAO {
    public interface ITourLogDAO {
        TourLog FindById(int tourLogId);
        TourLog AddNewTourLog(Tour logTour,string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance);
        void DeleteTourLog(TourLog tourLog);
        TourLog EditTourLog(TourLog tourLog, string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance);
        IEnumerable<TourLog> GetTourLogs(Tour tour);
        IEnumerable<TourLog> GetAllLogs();
    }
}
