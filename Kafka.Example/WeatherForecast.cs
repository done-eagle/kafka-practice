public class WeatherForecast(int Id, DateOnly Date, int TemperatureC, string? Summary)
{
    public int Id { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public DateOnly Date { get; init; } = Date;
    public int TemperatureC { get; init; } = TemperatureC;
    public string? Summary { get; init; } = Summary;

    public void Deconstruct(int id, out DateOnly Date, out int TemperatureC, out string? Summary)
    {
        Id = id;
        Date = this.Date;
        TemperatureC = this.TemperatureC;
        Summary = this.Summary;
    }
}