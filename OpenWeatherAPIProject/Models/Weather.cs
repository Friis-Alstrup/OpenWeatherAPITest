namespace OpenWeatherAPIProject.Models
{
    public class Weather
    {
        public Weather(int id, string main, string description, string icon)
        {
            this.id = id;
            this.main = main;
            this.description = description;
            this.icon = icon;
        }

        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}
