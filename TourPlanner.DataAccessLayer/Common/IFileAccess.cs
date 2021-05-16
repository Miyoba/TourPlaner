using System;
using System.Collections.Generic;
using System.IO;
using TourPlanner.Models;

namespace TourPlanner.DataAccessLayer.Common {
    public interface IFileAccess
    {
        int CreateNewTourFile(string tourName, string tourFromLocation, string tourToLocation, string tourDescription, int tourDistance);
        int CreateNewTourLogFile(Tour logTour,DateTime dateTime, string report, int distance, string totalTime, int rating);
        IEnumerable<FileInfo> SearchFiles(string searchTerm, TourTypes searchType);
        IEnumerable<FileInfo> GetAllFiles(TourTypes searchType);
    }
}
