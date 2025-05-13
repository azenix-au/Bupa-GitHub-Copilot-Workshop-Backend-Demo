using WeatherForecastDrinks.Models;

namespace WeatherForecastDrinks.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly ICoffeeService _coffeeService;

    // Constructor to initialize the WeatherForecastService with a CoffeeService dependency
    public WeatherForecastService(ICoffeeService coffeeService)
    {
        _coffeeService = coffeeService;
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    // Method to generate a list of weather forecasts for the next 5 days
    public IEnumerable<WeatherForecast> GetWeatherForecasts()
    {
        return Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                Summaries[Random.Shared.Next(Summaries.Length)]
            )).ToArray();
    }

    // Method to get recommended coffee drinks based on the provided weather summary
    public async Task<IEnumerable<CoffeeResponse>> GetRecommendedDrink(string weatherSummary)
    {
        var coffeeResponse =  await _coffeeService.GetCoffeeAsync();

        // DEMO 5 - Explain Linq query
        // DEMO 6 - Fix Linq query
        var recommendedCoffee = coffeeResponse.Where(x => string.Equals(x.Weather, weatherSummary)).ToList();

        return recommendedCoffee;
    }
}