using Newtonsoft.Json;

namespace CleveroadWeatherBackend.Models.Dto;

public class WeatherForecastDto : WeatherBaseDto
{
    /// <summary>
    /// Minimal temperature (Celsius)
    /// </summary>
    [JsonProperty("temp_min")]
    public double TemperatureMin { get; set; }
    /// <summary>
    /// Maximal temperature (Celsius)
    /// </summary>
    [JsonProperty("temp_max")]
    public double TemperatureMax { get; set; }
}