using System.Globalization;
using System.Net;
using CleveroadWeatherBackend.Models.Configuration;
using CleveroadWeatherBackend.Models.Dto;
using CleveroadWeatherBackend.Models.Requests.CurrentWeather;
using CleveroadWeatherBackend.Models.Requests.FiveDayForecast;
using CleveroadWeatherBackend.Tools;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CleveroadWeatherBackend.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly OpenWeatherMapOptions _options;
    private readonly HttpClient _client = new()
    {
        BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/")
    };

    public WeatherRepository(
        IOptions<OpenWeatherMapOptions> options)
    {
        _options = options.Value;
    }
    public async Task<IEnumerable<WeatherForecastDto>?> GetForecast5Days(string name)
    {
        // Web request
        using var httpRequest =
            await _client.GetAsync(
                $"forecast?appid={_options.ApiKey}&lang={_options.Language}&units={_options.Units}&q={name}");
        httpRequest.EnsureSuccessStatusCode();
        var response = await httpRequest.Content.ReadAsStringAsync();

        // Deserialize JSON
        var forecastResponse =
            JsonConvert.DeserializeObject<ForecastResponse>(response); // jsonWeatherForecast5Days
        if (forecastResponse == null) return null;
        var forecastResponseWeatherList = forecastResponse.WeatherList!;

        // Retrieving first 5 days
        var forecastDatesSeq = forecastResponseWeatherList
            .Select(x => DateTimeConverter.UnixDateTimeToUtc(x.Date).Date)
            .Distinct();
        forecastDatesSeq = forecastDatesSeq.Take(5);
        
        // Result set
        var resultWeatherForecastDtoSeq = new List<WeatherForecastDto>();
            
        // Foreach-dates
        foreach (var forecastDate in forecastDatesSeq)
        {
            // Get this day's forecasts
            var relatedForecastsSeq = forecastResponseWeatherList
                .Where(x => DateTimeConverter.UnixDateTimeToUtc(x.Date).Date == forecastDate)
                .ToList();
            // Add forecast with min, max and average values
            resultWeatherForecastDtoSeq.Add(
                new WeatherForecastDto
                {
                    Date = forecastDate.ToString(CultureInfo.CurrentCulture),
                    Cloudiness = (int)relatedForecastsSeq.Average(x => x.Clouds!.All),
                    WindSpeed = Math.Round(relatedForecastsSeq.Average(x => x.Wind!.Speed),2),
                    TemperatureMin = relatedForecastsSeq.Min(x => x.Main!.TemperatureMin),
                    TemperatureMax = relatedForecastsSeq.Max(x => x.Main!.TemperatureMax)
                });
        }
        return resultWeatherForecastDtoSeq;
    }

    public async Task<WeatherCurrentDto?> GetCurrentWeather(string name)
    {
        // Web request
        using var httpRequest =
            await _client.GetAsync(
                $"weather?appid={_options.ApiKey}&lang={_options.Language}&units={_options.Units}&q={name}");
        httpRequest.EnsureSuccessStatusCode();
        var response = await httpRequest.Content.ReadAsStringAsync();
        
        // Deserialize JSON
        var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
        if (weatherResponse == null) return null;
        
        // Return DTO of weather
        return new WeatherCurrentDto
        {
            Date = DateTimeConverter.UnixDateTimeToUtc(weatherResponse.Date).ToString(CultureInfo.CurrentCulture),
            Cloudiness = weatherResponse.Clouds!.All,
            WindSpeed = weatherResponse.Wind!.Speed,
            TemperatureCurrent = weatherResponse.Main!.TemperatureCurrent
        };
    }
}