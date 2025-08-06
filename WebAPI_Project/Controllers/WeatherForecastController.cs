using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Project.Models;

namespace WebAPI_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly PetDbContext _petDbContext;
        private static readonly string[] Summaries = new[]
        {
           "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(PetDbContext petDbContext, ILogger<WeatherForecastController> logger)
        {
           _petDbContext=petDbContext;
            _logger = logger;
            
            
        }
        //public async Task<ActionResult<IEnumerable<Pet>>> GetAll()
        //{
        //    return await _petDbContext.Pets.ToListAsync();
        //}

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}
