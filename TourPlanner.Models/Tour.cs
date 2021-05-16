namespace TourPlanner.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public int Distance { get; set; }

        public Tour(int id, string name, string description, string from, string to, int distance)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.FromLocation = from;
            this.ToLocation = to;
            this.Distance = distance;
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