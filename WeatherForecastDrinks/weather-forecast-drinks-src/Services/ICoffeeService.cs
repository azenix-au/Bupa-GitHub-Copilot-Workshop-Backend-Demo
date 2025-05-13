namespace WeatherForecastDrinks.Services;

public interface ICoffeeService
{
    Task<List<Models.CoffeeResponse>> GetCoffeeAsync();
}