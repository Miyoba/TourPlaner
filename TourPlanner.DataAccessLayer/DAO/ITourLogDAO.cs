using System;
using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.DAO {
    public interface ITourLogDAO {
        TourLog FindById(int tourLogId);
        TourLog AddNewTourLog(Tour logTour,string dateTime, string report, int distance, string totalTime, int rating);
        void DeleteTourLog(TourLog tourLog);
        TourLog EditTourLog(TourLog tourLog, string dateTime, string report, int distance, string totalTime, int rating);
        IEnumerable<TourLog> GetTourLogs(Tour tour);
    }
}
