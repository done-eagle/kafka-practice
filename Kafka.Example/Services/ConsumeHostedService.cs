using Confluent.Kafka;
using Kafka.Example.Entities;

namespace Kafka.Example.Services;

public class ConsumeHostedService : BackgroundService
{
	private readonly IConsumer<int, WeatherForecast> _consumer;
	private readonly ILogger<ConsumeHostedService> _logger;
	private const string TopicName = "supply_ship_event";

	public ConsumeHostedService(IConsumer<int, WeatherForecast> consumer, ILogger<ConsumeHostedService> logger)
	{
		_consumer = consumer;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		await Task.Yield();
		_consumer.Subscribe(TopicName);

		while (!stoppingToken.IsCancellationRequested)
		{
			var message = _consumer.Consume(stoppingToken);
			_logger.LogInformation("MessageId = {Id}, Value = {Summary}", message.Message.Key, message.Message.Value.Summary);
		}
		_consumer.Unsubscribe();
	}
}