using CleveroadWeatherBackend.Models.Dto;

namespace CleveroadWeatherBackend.Repositories;

public interface IWeatherRepository
{
    public Task<IEnumerable<WeatherForecastDto>?> GetForecast5Days(string name);
    public Task<WeatherCurrentDto?> GetCurrentWeather(string name);
}