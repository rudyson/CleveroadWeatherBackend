using Newtonsoft.Json;

namespace CleveroadWeatherBackend.Models.Dto;

public class WeatherCurrentDto : WeatherBaseDto
{
    /// <summary>
    /// Current temperature (Celsius)
    /// </summary>
    [JsonProperty("temp_cur")]
    public double TemperatureCurrent { get; set; }
}