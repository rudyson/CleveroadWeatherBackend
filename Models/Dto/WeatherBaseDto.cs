using Newtonsoft.Json;

namespace CleveroadWeatherBackend.Models.Dto;

public class WeatherBaseDto
{
    /// <summary>
    /// Date of weather request, UTC (ISO 8601)
    /// </summary>
    [JsonProperty("date")]
    public string? Date { get; set; }
    /// <summary>
    /// Metric wind speed (meter/sec)
    /// </summary>
    [JsonProperty("wind_speed")]
    public double WindSpeed { get; set; }
    /// <summary>
    /// Cloudiness provided in percents
    /// </summary>
    [JsonProperty("cloudiness")]
    public int Cloudiness { get; set; }
}