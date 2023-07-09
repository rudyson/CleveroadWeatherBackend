using CleveroadWeatherBackend.Models.Requests.CurrentWeather;
using Newtonsoft.Json;

namespace CleveroadWeatherBackend.Models.Requests.FiveDayForecast;

[JsonObject(MemberSerialization.OptIn)]
public class ForecastResponse
{
    [JsonProperty("list")]
    public List<WeatherResponse>? WeatherList { set; get; }
}