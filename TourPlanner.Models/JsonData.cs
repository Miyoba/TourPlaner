using System.Collections.Generic;

namespace TourPlanner.Models {
    public class JsonData {
        public IEnumerable<Tour> Tours { get; set; }
        public IEnumerable<TourLog> Logs { get; set; }
        
        public JsonData()
        {
            this.Tours = new List<Tour>();
            this.Logs = new List<TourLog>();
        }

        public JsonData(IEnumerable<Tour> tours, IEnumerable<TourLog> logs)
        {
            this.Tours = tours;
            this.Logs = logs;
        }
    }
}
