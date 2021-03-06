﻿using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface ITourPlannerFactory
    {
        IEnumerable<Tour> GetTours();
        IEnumerable<TourLog> GetTourLogs(Tour tour);
        IEnumerable<Tour> Search(string searchArg, bool caseSensitive = false);
        IEnumerable<TourLog> SearchTourLog(Tour tour, string searchArg, bool caseSensitive = false);
        Tour AddTour(string tourName, string tourDescription, string tourFromLocation, string tourToLocation, int tourDistance);
        TourLog AddTourLog(Tour tour, string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance);
        void DeleteTour(Tour tour, string imagePath);
        void DeleteTourLog(TourLog tourLog);
        Tour EditTour(Tour tour, string tourName, string tourDescription, string tourFromLocation, string tourToLocation, int tourDistance);
        TourLog EditTourLog(TourLog tourLog, string dateTime, string report, int distance, string totalTime, int rating, string vehicle, int avgSpeed, string people, int breaks, int linearDistance);
        bool ExportData();
        bool ImportData();
        bool PrintData(Tour currentTour);
        bool PrintAllData();
    }
}
