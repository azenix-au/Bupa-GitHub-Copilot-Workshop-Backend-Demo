namespace WeatherForecastDrinks.Services;

public class CoffeeService : ICoffeeService
{
    private readonly HttpClient _httpClient;

    public CoffeeService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        // Log the BaseAddress during CoffeeService initialization
        Console.WriteLine($"CoffeeService initialized with BaseAddress: {_httpClient.BaseAddress}");

        // Add a null check for BaseAddress
        if (_httpClient.BaseAddress == null)
        {
            throw new InvalidOperationException("HttpClient BaseAddress is not set. Ensure it is configured correctly in Program.cs.");
        }
    }

    public async Task<List<Models.CoffeeResponse>> GetCoffeeAsync()
    {
        // Explicitly construct the full URI using BaseAddress
        var response = await _httpClient.GetAsync("/coffee");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        // DEMO 3 - Get help with debugging statements
        // ...
        // DEMO 4 - Fix serialization
        // ...
        var coffeeList = System.Text.Json.JsonSerializer.Deserialize<List<Models.CoffeeResponse>>(content);
        return coffeeList ?? new List<Models.CoffeeResponse>();
    }
}