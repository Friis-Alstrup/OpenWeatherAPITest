using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using OpenWeatherAPIProject.Models;

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

                HttpResponseMessage message = await client.GetAsync("weather/forecast");

                if (message.IsSuccessStatusCode)
                {
                    var response = message.Content.ReadAsStringAsync().Result;
                    Root? weather = JsonConvert.DeserializeObject<Root>(response);
                    return View(weather);
                }

            }
            return NotFound();
        }
    }
}
