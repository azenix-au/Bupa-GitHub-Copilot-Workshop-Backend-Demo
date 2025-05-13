using Microsoft.AspNetCore.Mvc;
using WeatherForecastDrinks.Services;
using WeatherForecastDrinks.Models;

namespace WeatherForecastDrinks.Controllers;

[ApiController]
[Route("api/")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    /// <summary>
    /// Gets a list of weather forecasts.
    /// </summary>
    /// <returns>A list of weather forecasts.</returns>
    [HttpGet]
    public IEnumerable<WeatherForecast> GetWeatherForecasts()
    {
        return _weatherForecastService.GetWeatherForecasts();
    }

    [HttpGet("{weatherSummary}")]
    public async Task<IEnumerable<CoffeeResponse>> GetRecommendedDrink(string weatherSummary)
    {
        return  await _weatherForecastService 
            .GetRecommendedDrink(weatherSummary);    
    }
}