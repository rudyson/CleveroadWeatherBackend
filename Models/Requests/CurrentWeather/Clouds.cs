using Newtonsoft.Json;

namespace CleveroadWeatherBackend.Models.Requests.CurrentWeather;

[JsonObject(MemberSerialization.OptIn)]
public class Clouds
{
    /// <summary>
    /// Cloudiness, percent value
    /// </summary>
    [JsonProperty("all")]
    public int All { get; set; }
}