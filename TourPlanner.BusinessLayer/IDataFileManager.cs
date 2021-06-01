using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface IDataFileManager
    {
        public bool ExportData(IEnumerable<Tour> tours, IEnumerable<TourLog> logs);
        public JsonData ImportData();
    }
}