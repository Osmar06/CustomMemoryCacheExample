using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomMemoryCacheExample.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomMemoryCacheExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var  weathers = Summaries.Select((sumary, index) => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = sumary
            })
            .ToArray();

            foreach (var item in weathers)
                MemoryCache.Add(item.Summary, item);            

            return weathers;
        }

        [HttpGet("{key}")]
        public WeatherForecast Get(string key) => MemoryCache.Get<WeatherForecast>(key);
    }
}
