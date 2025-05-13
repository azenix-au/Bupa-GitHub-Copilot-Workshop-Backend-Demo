# Overview 
This is a sample API application that was created by running `dotnet new webapi` and using GH copilot to manipulate and update to the current state. 

# Demo 
There are commented sections throughout the code to practice some prompts as showing in the co pilot training demo.

## Demo 1 - Fix service registration

Run `dotnet run` and there will be errors in the terminal. Copilot can help check the errors in console and can run the commands for you and iteratively work through the errors.

Navigate to `Program.cs` to set the context.

> Sample prompt: "Can you please help me run my project with `dotnet run`" 

You will need to monitor the output as this can vary - ultimately it should register the `CoffeeService` and add a http client for it.

<details>

<summary>Solution: Add HTTP Client </summary>

```c#
// Register CoffeeService
builder.Services.AddHttpClient<ICoffeeService, CoffeeService>(client =>
{
    var baseAddress = builder.Configuration["ApiSettings:CoffeeApiBaseUrl"];
    Console.WriteLine($"Setting HttpClient BaseAddress to: {baseAddress}");
    if (!string.IsNullOrEmpty(baseAddress))
    {
        client.BaseAddress = new Uri(baseAddress);
        Console.WriteLine($"HttpClient BaseAddress set to: {client.BaseAddress}");
    }
    else
    {
        throw new InvalidOperationException("CoffeeApiBaseUrl is not configured.");
    }
});
```
</details>

The app should be starting now.

## Demo 2 - Fix api route

Using something like Postman, call the endpoint do a call: `curl --location 'http://localhost:5134/recommendedDrink/hot'`. You should get a `404 Not Found` response. This scenario is when a new endpoint (`/recommendedDrink/{weatherSummary}`) is added but the route is not yet updated.

In `Program.cs` use copilot to help add new routes.

> Sample prompt: "Can you register the new route that is in WeatherForecastService.cs"

It should add the new RouteEndpoint.

<details>

<summary>Solution: Add new RouteEndpoint </summary>

From comment `DEMO 2 - Fix API route`

``` c#
// DEMO 2 - Fix API route
// Add a route for recommended drinks based on weather
app.MapGet("/recommendedDrink/{weatherSummary}", async (string weatherSummary, IWeatherForecastService weatherService) =>
{
    return await weatherService.GetRecommendedDrink(weatherSummary);
})
.WithName("GetRecommendedDrink")
.WithOpenApi();
```
</details>

App should be starting. Calling the endpoint now gives a `200OK` with body `[]`.

## Demo 3 -  Get help with debugging statements

Navigate to `CoffeeService.cs`. There is an issue with getting a response object. First try have a look if deserialization of the `CoffeeResponse` model is contributing to the issue. Use Copilot to add debug statements to aid with your debugging.

> Sample prompt: "Can you please help me debug the deserialization of response into CoffeeResponse"

Co Pilot should add some debug statements to assist. 

<details>

<summary> Sample: Debug statements </summary>

Sample of the debugging statements added

```c#
        // Log the raw JSON content for debugging
        Console.WriteLine("Raw JSON content:");
        Console.WriteLine(content);

        try
        {
            // Attempt to deserialize the JSON content
            var coffeeList = System.Text.Json.JsonSerializer.Deserialize<List<Models.CoffeeResponse>>(content);
            return coffeeList ?? new List<Models.CoffeeResponse>();
        }
        catch (System.Text.Json.JsonException ex)
        {
            // Log the exception details
            Console.WriteLine("Deserialization failed:");
            Console.WriteLine(ex.Message);
            throw;
        }
```
</details>

## Demo 4 - Fix serialization

Still in `CoffeeService.cs`. Now that you have done some debugging, you know that the deserialization is the issue. Get CoPilot to fix this issue.

> Sample prompt: "The response is not deserializing properly. Can you please fix this."

You may need to be more specific with how you want it fixed. E.g. specify if you want to use JsonSerializerOptions or to use attributes on your model.

<details>

<summary> Solution: Fixed deserialization + debugging statements</summary>

```c#
        // Log the raw JSON content for debugging
        Console.WriteLine("Raw JSON content:");
        Console.WriteLine(content);

        try
        {
            // Configure JsonSerializerOptions to ignore case
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Attempt to deserialize the JSON content
            var coffeeList = System.Text.Json.JsonSerializer.Deserialize<List<Models.CoffeeResponse>>(content, options);
            return coffeeList ?? new List<Models.CoffeeResponse>();
        }
        catch (System.Text.Json.JsonException ex)
        {
            // Log the exception details
            Console.WriteLine("Deserialization failed:");
            Console.WriteLine(ex.Message);
            throw;
        }
```
</details>

Call the endpoint in Postman again. You should be getting the `200 OK` with empty response body `[]` but debugging statements should show deserialization is working properly now.

## Demo 5 - Explain Linq query
Now look at the linq query in `WeatherForecastService.cs`

Hover over line 39 (linq query) and open copilot inline (`CMD` + `i`). Ask github to explain the linq query. 

> Sample prompt: explain this linq query
> Sample prompt: explain this linq query like I'm 5

## Demo 6 - Fix Linq query

Afer understanding the linq query, you now know that it is the case sensitive comparison that is causing the mismatch. Ask co pilot to make the query case insensitive. 

> Sample prompt: Can you please fix this Linq query to be case insensitive.

<details>

<summary>Solution: Case-insenstivie comparison</summary>

```c#
        // Update LINQ query to perform case-insensitive comparison
        var recommendedCoffee = coffeeResponse.Where(x => string.Equals(x.Weather, weatherSummary, StringComparison.OrdinalIgnoreCase)).ToList();
```
</details>

## Demo 7 - Add more mock data for testing

Navigate to the `Services/CoffeeServiceTests.cs` which has sample tests for the http client service.

Ask co pilot to expand your response mock data to contain 10 items.

> Sample prompt: Can you please update this mock data to return 10 items in the response.

<details>

<summary>Solution: 10 items in response data</summary>

```c#
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(new List<CoffeeResponse>
                {
                    new CoffeeResponse { Id = "1", Title = "Espresso" },
                    new CoffeeResponse { Id = "2", Title = "Latte" },
                    new CoffeeResponse { Id = "3", Title = "Cappuccino" },
                    new CoffeeResponse { Id = "4", Title = "Americano" },
                    new CoffeeResponse { Id = "5", Title = "Mocha" },
                    new CoffeeResponse { Id = "6", Title = "Macchiato" },
                    new CoffeeResponse { Id = "7", Title = "Flat White" },
                    new CoffeeResponse { Id = "8", Title = "Affogato" },
                    new CoffeeResponse { Id = "9", Title = "Irish Coffee" },
                    new CoffeeResponse { Id = "10", Title = "Cold Brew" }
                })
            });
```
</details>

## Demo 8 - Expand mock data for testing

In the scenario where your model has been updated/expanded with more fields, you can ask co pilot to create more test data based off these new fields.

Look at the definition of `CoffeeResponse`. The test mock data only supplies `Id` and `Title`. Get co pilot to update the data to utilise all the fields in the model.

> Sample prompt: "Update these mocks to have data for every field in CoffeeResponse"

<details>

<summary> Solution: Expanded mock data for testing </summary>

```c#
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(new List<CoffeeResponse>
                {
                    new CoffeeResponse { Id = "1", Title = "Espresso", Description = "Strong and bold coffee", Ingredients = new List<string> { "Coffee" }, Category = "Hot", Weather = "Cold" },
                    new CoffeeResponse { Id = "2", Title = "Latte", Description = "Smooth and creamy", Ingredients = new List<string> { "Coffee", "Milk" }, Category = "Hot", Weather = "Cold" },
                    new CoffeeResponse { Id = "3", Title = "Cappuccino", Description = "Rich and foamy", Ingredients = new List<string> { "Coffee", "Milk", "Foam" }, Category = "Hot", Weather = "Cold" },
                    new CoffeeResponse { Id = "4", Title = "Americano", Description = "Diluted espresso", Ingredients = new List<string> { "Coffee", "Water" }, Category = "Hot", Weather = "Cold" },
                    new CoffeeResponse { Id = "5", Title = "Mocha", Description = "Chocolate flavored coffee", Ingredients = new List<string> { "Coffee", "Milk", "Chocolate" }, Category = "Hot", Weather = "Cold" },
                    new CoffeeResponse { Id = "6", Title = "Macchiato", Description = "Espresso with a dash of milk", Ingredients = new List<string> { "Coffee", "Milk" }, Category = "Hot", Weather = "Cold" },
                    new CoffeeResponse { Id = "7", Title = "Flat White", Description = "Smooth and velvety", Ingredients = new List<string> { "Coffee", "Milk" }, Category = "Hot", Weather = "Cold" },
                    new CoffeeResponse { Id = "8", Title = "Affogato", Description = "Espresso over ice cream", Ingredients = new List<string> { "Coffee", "Ice Cream" }, Category = "Hot", Weather = "Cold" },
                    new CoffeeResponse { Id = "9", Title = "Irish Coffee", Description = "Coffee with whiskey", Ingredients = new List<string> { "Coffee", "Whiskey", "Cream" }, Category = "Hot", Weather = "Cold" },
                    new CoffeeResponse { Id = "10", Title = "Cold Brew", Description = "Smooth and cold", Ingredients = new List<string> { "Coffee", "Water" }, Category = "Cold", Weather = "Hot" }
                })
            });

```
</details>