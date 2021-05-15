namespace TourPlanner.Models
{
    public class Tour
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RouteInformation { get; set; }
        public int Distance { get; set; }

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