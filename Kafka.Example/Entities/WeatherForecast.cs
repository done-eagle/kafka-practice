namespace Kafka.Example.Entities;

public class WeatherForecast(int Id, DateOnly Date, int TemperatureC, string? Summary)
{
	public int Id { get; set; } = Id;
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
	public DateOnly Date { get; init; } = Date;
	public int TemperatureC { get; init; } = TemperatureC;
	public string? Summary { get; init; } = Summary;

	public void Deconstruct(int id, out DateOnly date, out int temperatureC, out string? summary)
	{
		Id = id;
		date = this.Date;
		temperatureC = this.TemperatureC;
		summary = this.Summary;
	}
}