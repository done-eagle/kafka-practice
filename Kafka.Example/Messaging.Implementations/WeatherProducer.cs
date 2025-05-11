using Kafka.Example.Messaging.Abstractions;
using Confluent.Kafka;

namespace Kafka.Example.Messaging.Implementations;

internal class WeatherProducer : IWeatherProducer
{
    private readonly IProducer<int, WeatherForecast> _producer;

    public WeatherProducer(IProducer<int, WeatherForecast> producer)
    {
        _producer = producer;
    }
    
    public void Publish(WeatherForecast weatherForecast)
    {
        var message = new Message<int, WeatherForecast>();

        message.Key = weatherForecast.Id;
        message.Value = weatherForecast;
        
        _producer.Produce("supply_ship_event", message);
    }
}