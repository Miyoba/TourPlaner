using System.Collections.Generic;
using TourPlanner.Models;

namespace TourPlanner.BusinessLayer
{
    public interface IReportGenerator
    {
        public bool GeneratePDFReportForTours(IEnumerable<Tour> tours, IEnumerable<TourLog> logs, bool summarize);
    }
}