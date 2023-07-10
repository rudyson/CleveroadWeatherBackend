using Microsoft.Extensions.Options;

namespace CleveroadWeatherBackend.Models.Configuration;

public class OpenWeatherMapConfigureOptions : IConfigureOptions<OpenWeatherMapOptions>
{
    private const string SectionName = "OpenWeatherMap";
    private readonly IConfiguration _configuration = null!;

    public OpenWeatherMapConfigureOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public OpenWeatherMapConfigureOptions()
    {
    }

    public void Configure(OpenWeatherMapOptions options)
    {
        _configuration
            .GetSection(SectionName)
            .Bind(options);
    }
}