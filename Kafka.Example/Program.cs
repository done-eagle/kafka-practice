using Confluent.Kafka;
using Kafka.Example.Messaging.Abstractions;
using Kafka.Example.Messaging.Implementations;
using Kafka.Example;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    builder.SetValueSerializer(new JsonSerializer<WeatherForecast>());
    
    return builder.Build();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();