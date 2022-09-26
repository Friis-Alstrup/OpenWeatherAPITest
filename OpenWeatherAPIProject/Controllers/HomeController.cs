using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenWeatherAPIProject.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OpenWeatherAPIProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7181");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("weather/today");

                if (message.IsSuccessStatusCode)
                {
                    var response = message.Content.ReadAsStringAsync().Result;
                    Root? weather = JsonConvert.DeserializeObject<Root>(response);
                    ViewData["Icon"] = "http://openweathermap.org/img/wn/" + weather?.weather.First().icon + "@2x.png";

                    return View(weather);
                }
            }

            return View();
        }
    }
}