using System.Net.Http;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading;
using Moq.Protected;
using WeatherForecastDrinks.Services;
using WeatherForecastDrinks.Models;

namespace WeatherForecastDrinks.Tests.Services;

public class CoffeeServiceTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly CoffeeService _coffeeService;

    public CoffeeServiceTests()
    {
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _configurationMock = new Mock<IConfiguration>();

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                // DEMO 7 - Add more mock data for testing
                // ...
                // DEMO 8 - Expand mock data for testing
                // ...
                Content = JsonContent.Create(new List<CoffeeResponse>
                {
                    new CoffeeResponse { Id = "1", Title = "Espresso"},
                    new CoffeeResponse { Id = "2", Title = "Latte"}
                })
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://example.com")
        };

        _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        _coffeeService = new CoffeeService(httpClient, _configurationMock.Object);
    }

    [Fact]
    public async Task GetCoffeeAsync_ReturnsExpectedResult()
    {
        // Act
        var result = await _coffeeService.GetCoffeeAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<CoffeeResponse>>(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Espresso", result[0].Title);
        Assert.Equal("Latte", result[1].Title);
    }

    // Add more test methods to cover other functionalities of CoffeeService
}