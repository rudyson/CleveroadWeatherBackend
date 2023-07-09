using CleveroadWeatherBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CleveroadWeatherBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherRepository _repository;

        public WeatherController(IWeatherRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Current weather in the provided city
        /// </summary>
        /// <param name="name">Name of city (region and country are additional to specify location)</param>
        /// <returns></returns>
        /// <response code="200">Successful response</response>
        /// <response code="400">Missing arguments</response>
        /// <response code="401">Wrong API key</response>
        /// <response code="404">City not found</response>
        /// <response code="429">API key's requests limit reached</response>
        /// <response code="500">Contact OpenWeather API support</response>
        /// <response code="501">Contact OpenWeather API support</response>
        /// <response code="502">Contact OpenWeather API support</response>
        /// <response code="503">Contact OpenWeather API support</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentWeather(string name)
        {
            var response = await _repository.GetCurrentWeather(name);
            if (response == null) return BadRequest("Make sure your API key is valid");
            return Ok(response);
        }
        /// <summary>
        /// Returns weather forecast for 5 days in selected city
        /// </summary>
        /// <param name="name">Name of city (region and country are additional to specify location)</param>
        /// <returns></returns>
        /// <response code="200">Successful response</response>
        /// <response code="400">Missing arguments</response>
        /// <response code="401">Wrong API key</response>
        /// <response code="404">City not found</response>
        /// <response code="429">API key's requests limit reached</response>
        /// <response code="500">Contact OpenWeather API support</response>
        /// <response code="501">Contact OpenWeather API support</response>
        /// <response code="502">Contact OpenWeather API support</response>
        /// <response code="503">Contact OpenWeather API support</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        [HttpGet("forecast")]
        public async Task<IActionResult> GetForecast(string name)
        {
            var response = await _repository.GetForecast5Days(name);
            if (response == null) return BadRequest("Make sure your API key is valid");
            return Ok(response);
        }
    }
}
