using Kafka.Example.Entities;

namespace Kafka.Example.Messaging.Abstractions;

public interface IWeatherProducer
{
	void Publish(WeatherForecast weatherForecast);
}