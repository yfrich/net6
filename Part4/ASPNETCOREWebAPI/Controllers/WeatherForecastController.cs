using ASPNETCOREWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCOREWebAPI.Controllers
{
    [ApiController]
    //[Route("haha/[controller]")]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Write()
        {
            System.IO.File.WriteAllText("f:/1.txt", "¹þ¹þ¹þ");
        }
        [HttpGet]
        public string Hello()
        {
            return "Hello";
        }
        [HttpGet]
        public int GetCJ(int id)
        {
            if (id == 1)
            {
                return 88;
            }
            else if (id == 2)
            {
                return 99;
            }
            else
            {
                throw new Exception("Id´íÎó");
            }
        }
        [HttpGet]
        public ActionResult<int> GetCJ2(int id)
        {
            if (id == 1)
            {
                return 88;
            }
            else if (id == 2)
            {
                return 99;
            }
            else
            {
                return NotFound("id´íÎó");
                //throw new Exception("Id´íÎó");
            }
        }
    }
}
