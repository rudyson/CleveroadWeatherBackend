using Newtonsoft.Json;

namespace CleveroadWeatherBackend.Models.Requests.CurrentWeather;

[JsonObject(MemberSerialization.OptIn)]
public class Wind
{
    /// <summary>
    /// Wind speed. Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.
    /// </summary>
    [JsonProperty("speed")]
    public double Speed { get; set; }
}