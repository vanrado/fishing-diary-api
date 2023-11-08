namespace FishingDiary.API.Models
{
    public class WeatherDto
    {
        public float Temperature { get; set; }
        public float WindSpeed { get; set; }
        public float CloudCover { get; set; }
        public float AtmosphericPressure { get; set; }
        public string MoonPhase { get; set; }
    }
}
