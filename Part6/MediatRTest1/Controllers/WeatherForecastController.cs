using EFCore实体属性操作的秘密1;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatRTest1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly MyDbContext dbContext;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator, MyDbContext dbContext)
        {
            _logger = logger;
            this.mediator = mediator;
            this.dbContext = dbContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            User u1 = new User("rich");
            u1.ChangePasswordHash("123456");
            u1.ChangeUserName("我是啊哈哈哈");
            dbContext.Users.Add(u1);
            await dbContext.SaveChangesAsync();

            //await mediator.Publish(new PostNotification("ss"));

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
