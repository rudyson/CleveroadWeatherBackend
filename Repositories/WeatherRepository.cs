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
    private const string ApiRoute = "https://api.openweathermap.org/data/2.5/";
    private readonly OpenWeatherMapOptions _options;
    private readonly ILogger<WeatherRepository> _logger;

    public WeatherRepository(
        IOptions<OpenWeatherMapOptions> options,
        ILogger<WeatherRepository> logger)
    {
        _options = options.Value;
        _logger = logger;
    }
    public async Task<IEnumerable<WeatherForecastDto>?> GetForecast5Days(string name)
    {
        try
        {
            // Web request
            var url = $"{ApiRoute}forecast?appid={_options.ApiKey}&lang={_options.Language}&units={_options.Units}&q={name}";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;
            using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = await streamReader.ReadToEndAsync();
            }
            // Deserialize JSON
            var forecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(response); // jsonWeatherForecast5Days
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
                    .Where(x => DateTimeConverter.UnixDateTimeToUtc(x.Date).Date==forecastDate)
                    .ToList();
                // Add forecast with min, max and average values
                resultWeatherForecastDtoSeq.Add(
                    new WeatherForecastDto
                    {
                        Date = forecastDate.ToString(CultureInfo.CurrentCulture),
                        Cloudiness = (int)relatedForecastsSeq.Average(x => x.Clouds!.All),
                        WindSpeed = relatedForecastsSeq.Average(x => x.Wind!.Speed),
                        TemperatureMin = relatedForecastsSeq.Min(x => x.Main!.TemperatureMin),
                        TemperatureMax = relatedForecastsSeq.Max(x => x.Main!.TemperatureMax)
                    });
            }
            return resultWeatherForecastDtoSeq;
        }
        catch (JsonReaderException jsonReaderException)
        {
            _logger.LogError(jsonReaderException.ToString());
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }
        return null;
    }

    public async Task<WeatherCurrentDto?> GetCurrentWeather(string name)
    {
        try
        {
            // Web request
            var url = $"{ApiRoute}weather?appid={_options.ApiKey}&lang={_options.Language}&units={_options.Units}&q={name}";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;
            using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = await streamReader.ReadToEndAsync();
            }
            // Deserialize JSON
            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response); //jsonWeatherDay
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
        catch (JsonReaderException jsonReaderException)
        {
            _logger.LogError(jsonReaderException.ToString());
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
        }
        return null;
    }
}