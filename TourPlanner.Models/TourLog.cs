namespace TourPlanner.Models {
    public class TourLog {
        public string DateTime { get; set; }
        public string Report { get; set; }
        public int Distance { get; set; }
        public string TotalTime { get; set; }
        public int Rating { get; set; }

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
