using System;
using System.Dynamic;

namespace TourPlanner.Models {
    public class TourLog {
        public int Id  { get; set; }
        public Tour TourId { get; set; }
        public string DateTime { get; set; }
        public string Report { get; set; }
        public int Distance { get; set; }
        public string TotalTime { get; set; }
        public int Rating { get; set; }

        public TourLog(int id, Tour tourId, string dateTime, string report, int distance, string totalTime, int rating)
        {
            this.Id = id;
            this.TourId = tourId;
            this.DateTime = dateTime;
            this.Report = report;
            this.Distance = distance;
            this.TotalTime = totalTime;
            this.Rating = rating;
        }

        public string GetFieldValue(string fieldName, bool caseSensitive = false)
        {
            var ergObj = this.GetType().GetProperty(fieldName).GetValue(this, null);
            
            if (ergObj == null)
                return "";

            string erg = ergObj.ToString();
            return caseSensitive ? erg : erg.ToLower();
        }
    }
}
