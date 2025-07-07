using Confluent.Kafka;
using Kafka.Example.Messaging.Abstractions;
using Kafka.Example.Messaging.Implementations;
using Kafka.Example;
using Kafka.Example.Entities;
using Kafka.Example.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddControllers();
builder.Services.AddScoped<IWeatherProducer, WeatherProducer>();

// TODO Добавить обертку для инициализации Producer 
builder.Services.AddSingleton<IProducer<int, WeatherForecast>>(producer =>
{
    var config = new ProducerConfig
    {
        // адреса брокеров
        BootstrapServers = "localhost:29092"
    };
    var builder = new ProducerBuilder<int, WeatherForecast>(config);
    builder.SetValueSerializer(new MessageSerializer<WeatherForecast>());
    
    return builder.Build();
});

builder.Services.AddSingleton<IConsumer<int, WeatherForecast>>(producer =>
{
    var config = new ConsumerConfig
    {
        BootstrapServers = "localhost:29092",
        GroupId = "WeatherConsumer",
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnableAutoCommit = false
    };
    var builder = new ConsumerBuilder<int, WeatherForecast>(config);
    builder.SetValueDeserializer(new MessageSerializer<WeatherForecast>());
    
    return builder.Build();
});

builder.Services.AddHostedService<ConsumeHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();