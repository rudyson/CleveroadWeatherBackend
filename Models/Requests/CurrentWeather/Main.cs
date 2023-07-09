using Newtonsoft.Json;

namespace CleveroadWeatherBackend.Models.Requests.CurrentWeather;

[JsonObject(MemberSerialization.OptIn)]
public class Main
{
    /// <summary>
    /// Current temperature
    /// </summary>
    [JsonProperty("temp")]
    public double TemperatureCurrent { get; set; }
    /// <summary>
    /// Minimum temperature at the moment.
    /// This is minimal currently observed temperature (within large megalopolises and urban areas).
    /// </summary>
    [JsonProperty("temp_min")]
    public double TemperatureMin { get; set; }
    /// <summary>
    /// Maximum temperature at the moment.
    /// This is maximal currently observed temperature (within large megalopolises and urban areas).
    /// </summary>
    [JsonProperty("temp_max")]
    public double TemperatureMax { get; set; }
}