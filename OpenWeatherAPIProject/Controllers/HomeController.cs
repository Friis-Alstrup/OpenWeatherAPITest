using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using WeatherLibrary;

namespace OpenWeatherAPIProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(string? unit, string weatherType)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7181");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (unit == null) unit = "metric";
                if (weatherType == null) weatherType = "all";

                HttpResponseMessage message = await client.GetAsync($"weather/forecast?unit={unit}");

                if (message.IsSuccessStatusCode)
                {
                    var response = await message.Content.ReadAsStringAsync();
                    Root? weather = JsonSerializer.Deserialize<Root>(response);


                    if (weatherType == "all")
                    {
                    } else
                    {
                        foreach (var a in weather.list.ToList())
                        {
                            if (a.weather.First().description != weatherType) weather.list.Remove(a);
                        }
                    }

                    return View(weather);
                }

            }
            return NotFound();
        }
    }
}
