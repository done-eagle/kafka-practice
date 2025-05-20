using Confluent.Kafka;
using Kafka.Example.Entities;
using Kafka.Example.Messaging.Abstractions;

namespace Kafka.Example.Messaging.Implementations;

internal class WeatherProducer : IWeatherProducer
{
	private readonly IProducer<int, WeatherForecast> _producer;
	private const string TopicName = "supply_ship_event";

	public WeatherProducer(IProducer<int, WeatherForecast> producer)
	{
		_producer = producer;
	}

	public void Publish(WeatherForecast weatherForecast)
	{
		var message = new Message<int, WeatherForecast>();

		message.Key = weatherForecast.Id;
		message.Value = weatherForecast;

		_producer.Produce(TopicName, message);
	}
}