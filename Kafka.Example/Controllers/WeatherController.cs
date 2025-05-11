using Kafka.Example.Messaging.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Example.Controllers;

[ApiController]
[Route("api/example")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherProducer _producer;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public WeatherController(IWeatherProducer producer)
    {
        _producer = producer;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    index,
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]
                ))
            .ToArray();

        foreach (var weatherForecast in forecast)
        {
            _producer.Publish(weatherForecast);
        }
        
        return forecast;
    }
}