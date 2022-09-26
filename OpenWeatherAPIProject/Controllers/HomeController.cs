using Microsoft.AspNetCore.Mvc;
using OpenWeatherAPIProject.Models;
using System.Diagnostics;
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
            using HttpClient client = new()
            {
                BaseAddress = new Uri("https://api.openweathermap.org")
            };

            var responseMessage = await client.GetAsync("/data/2.5/weather?lat=44.34&lon=10.99&appid=aefafa7213efcac5d66b28365b99db4d");

            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

            Weather? weather = JsonSerializer.Deserialize<Weather>(jsonResponse);

            return View(weather);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}