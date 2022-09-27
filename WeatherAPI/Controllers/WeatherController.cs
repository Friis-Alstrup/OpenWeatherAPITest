using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("weather")]
    public class WeatherController : Controller
    {
        [HttpGet]
        [Route("forecast")]
        public async Task<ActionResult<Root>> Forecast()
        {
            string apiKey = "aefafa7213efcac5d66b28365b99db4d";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.openweathermap.org/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = await client.GetAsync($"/data/2.5/forecast?q=copenhagen&appid={apiKey}&units=metric");
                if (message.IsSuccessStatusCode)
                {
                    var response = message.Content.ReadAsStringAsync().Result;
                    Root? weather = JsonConvert.DeserializeObject<Root>(response);
                    return weather;
                } else
                {
                    return NotFound();
                }
            }
        }
    }
}
