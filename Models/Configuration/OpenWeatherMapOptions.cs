namespace CleveroadWeatherBackend.Models.Configuration;

public class OpenWeatherMapOptions
{
    public string? ApiKey { get; set; }
    public string? Language { get; set; } = "ua";
    public string? Units { get; set; } = "metric";
}