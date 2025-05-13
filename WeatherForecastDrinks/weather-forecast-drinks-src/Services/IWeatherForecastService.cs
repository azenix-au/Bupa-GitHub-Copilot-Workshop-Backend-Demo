using WeatherForecastDrinks.Models;

namespace WeatherForecastDrinks.Services;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> GetWeatherForecasts();
    Task<IEnumerable<CoffeeResponse>> GetRecommendedDrink(string weatherSummary);
}
