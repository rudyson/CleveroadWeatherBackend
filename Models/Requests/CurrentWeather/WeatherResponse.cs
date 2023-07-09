using Newtonsoft.Json;

namespace CleveroadWeatherBackend.Models.Requests.CurrentWeather;

[JsonObject(MemberSerialization.OptIn)]
public class WeatherResponse
{
    /// <summary>
    /// Time of data calculation, unix, UTC 
    /// </summary>
    [JsonProperty("dt")]
    public int Date { set; get; }
    /// <summary>
    /// Wind data
    /// </summary>
    [JsonProperty("wind")]
    public Wind? Wind { get; set; }
    /// <summary>
    /// Clouds data
    /// </summary>
    [JsonProperty("clouds")]
    public Clouds? Clouds { get; set; }
    [JsonProperty("main")]
    public Main? Main { get; set; }
}