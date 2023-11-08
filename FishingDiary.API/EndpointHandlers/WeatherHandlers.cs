using FishingDiary.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FishingDiary.API.EndpointHandlers
{
    public static class WeatherHandlers
    {
        public static async Task<Results<NotFound, Ok<WeatherDto>>> GetLocationWeather(double latitude, double longitude)
        {
            // Mock weather data for demonstration purposes
            var mockWeatherData = new WeatherDto
            {
                Temperature = 25.5f,           // in degrees Celsius
                WindSpeed = 10.2f,             // in meters per second
                CloudCover = 0.4f,             // percentage (0 to 1)
                AtmosphericPressure = 1013.2f, // in hPa
                MoonPhase = "Waxing Gibbous"   // moon phase description
            };
            return TypedResults.Ok(mockWeatherData);
        }
    }
}
