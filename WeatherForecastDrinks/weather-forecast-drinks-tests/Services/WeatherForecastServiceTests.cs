using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastDrinks.Models;
using System;
using WeatherForecastDrinks.Services;

namespace WeatherForecastDrinks.Tests.Services;

public class WeatherForecastServiceTests
{
    private readonly Mock<ICoffeeService> _coffeeServiceMock;
    private readonly WeatherForecastService _weatherForecastService;

    public WeatherForecastServiceTests()
    {
        _coffeeServiceMock = new Mock<ICoffeeService>();
        _weatherForecastService = new WeatherForecastService(_coffeeServiceMock.Object);
    }

    [Fact]
    public void GetWeatherForecasts_ReturnsForecasts()
    {
        // Act
        var result = _weatherForecastService.GetWeatherForecasts();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetRecommendedDrink_ReturnsDrinkBasedOnWeather()
    {
        // Arrange
        var weatherSummary = "Sunny";
        var expectedDrink = new List<CoffeeResponse>
        {
            new CoffeeResponse { Weather = "Sunny", Title = "Iced Coffee" }
        };

        _coffeeServiceMock.Setup(service => service.GetCoffeeAsync()).ReturnsAsync(expectedDrink);

        // Act
        var result = await _weatherForecastService.GetRecommendedDrink(weatherSummary);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedDrink.Count, result.Count());
        Assert.Equal(expectedDrink.First().Title, result.First().Title);
    }
}