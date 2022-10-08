using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherLibrary;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("weather")]
    public class ForecastController : ControllerBase
    {
        [HttpGet]
        [Route("forecast")]
        public async Task<ActionResult<Root>> Forecast(string? unit)
        {
            using HttpClient client = new HttpClient();

            string? apiKey = Environment.GetEnvironmentVariable("API_KEY");
            string city = "Odense";

            HttpResponseMessage message = await client.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units={unit}");
            message.EnsureSuccessStatusCode();

            return await message.Content.ReadFromJsonAsync<Root>();
        }
    }
}
