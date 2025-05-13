namespace WeatherForecastDrinks.Models;

public class CoffeeResponse
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<string>? Ingredients { get; set; }
    public string? Category { get; set; }
    public string? Id { get; set; }
    public string? Weather { get; set; }
}