using ASPNETCOREWebAPI.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace ASPNETCOREWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
        private readonly IConfiguration configuration;
        private readonly IOptionsSnapshot<TConfig> optionsSnapshot;
        private readonly IOptionsSnapshot<SmtpSettings> optionsSnapshot1;
        private readonly IConnectionMultiplexer connectionMultiplexer;
        public ConfigController(IWebHostEnvironment environment, IConfiguration configuration, IOptionsSnapshot<TConfig> optionsSnapshot, IOptionsSnapshot<SmtpSettings> optionsSnapshot1, IConnectionMultiplexer connectionMultiplexer)
        {
            this.environment = environment;
            this.configuration = configuration;
            this.optionsSnapshot = optionsSnapshot;
            this.optionsSnapshot1 = optionsSnapshot1;
            this.connectionMultiplexer = connectionMultiplexer;
        }

        /// <summary>
        /// 读取系统环境变量方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string? Demo1()
        {
            //return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return environment.EnvironmentName;
        }
        /// <summary>
        /// 通过配置读取信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string? Demo2()
        {
            return configuration.GetSection("connStr").Value;
        }
        /// <summary>
        /// 集中化配置信息读取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string? Demo3()
        {
            var ping = connectionMultiplexer.GetDatabase(0).Ping();
            return optionsSnapshot1.Value.ToString() + "|" + ping;
        }
    }
}
