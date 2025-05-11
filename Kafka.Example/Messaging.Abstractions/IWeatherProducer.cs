namespace Kafka.Example.Messaging.Abstractions;

public interface IWeatherProducer
{
    void Publish(WeatherForecast weatherForecast);
}