using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace demo.Controllers
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        [HttpGet]
        [ApiVersion("1", Deprecated=true)]//弃用，废弃；标记过时的Api为弃用状态；用Deprecated修饰控制器；仍然可以调用该版本，只是一种让 调用API 用户意识到以下版本在将来会被弃用。
        [Route("Weather")]
        public IEnumerable<WeatherForecast> GetV1()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Version = "1.0",
            })
            .ToArray();
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns></returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [Route("GetV11")]
        public IEnumerable<WeatherForecast> GetV11(DateTime date)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)],
                    Version = "1.0",
                })
                .ToArray();
        }
        [ApiVersionNeutral]//不指定Api版本号；随便访问；
        [HttpGet]
        [Route("Get")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)],
                    Version = "2.0",
                })
                .ToArray();
        }
        [HttpGet]
        [MapToApiVersion("2")]//支持多个版本的单控制器；
        [Route("Weather")]
        public IEnumerable<WeatherForecast> GetV2()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Version = "2.0",
            })
            .ToArray();
        }
    }
}
