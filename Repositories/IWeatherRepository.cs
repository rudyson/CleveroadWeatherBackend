using CleveroadWeatherBackend.Models.Dto;

namespace CleveroadWeatherBackend.Repositories;

public interface IWeatherRepository
{
    /// <summary>
    /// Gets weather forecast for 5 days in the provided city
    /// </summary>
    /// <param name="name">City name</param>
    /// <returns></returns>
    public Task<IEnumerable<WeatherForecastDto>?> GetForecast5Days(string name);
    /// <summary>
    /// Gets current weather in the provided city
    /// </summary>
    /// <param name="name">City name</param>
    /// <returns></returns>
    public Task<WeatherCurrentDto?> GetCurrentWeather(string name);
}