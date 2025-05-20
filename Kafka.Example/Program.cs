using Confluent.Kafka;
using Kafka.Example.Messaging.Abstractions;
using Kafka.Example.Messaging.Implementations;
using Kafka.Example;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddControllers();
builder.Services.AddScoped<IWeatherProducer, WeatherProducer>();

// TODO Добавить обертку для инициализации Producer 
builder.Services.AddSingleton<IProducer<int, Kafka.Example.Entities.WeatherForecast>>(producer =>
{
    var config = new ProducerConfig
    {
        // адреса брокеров
        BootstrapServers = "localhost:29092"
    };
    var builder = new ProducerBuilder<int, Kafka.Example.Entities.WeatherForecast>(config);
    builder.SetValueSerializer(new MessageSerializer<Kafka.Example.Entities.WeatherForecast>());
    
    return builder.Build();
});

builder.Services.AddSingleton<IConsumer<int, Kafka.Example.Entities.WeatherForecast>>(producer =>
{
    var config = new ConsumerConfig
    {
        BootstrapServers = "localhost:29092",
        GroupId = "WeatherConsumer",
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnableAutoCommit = false
    };
    var builder = new ConsumerBuilder<int, Kafka.Example.Entities.WeatherForecast>(config);
    builder.SetValueDeserializer(new MessageSerializer<Kafka.Example.Entities.WeatherForecast>());
    
    return builder.Build();
});

builder.Services.AddHostedService<Kafka.Example.Services.ConsumeHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();