using WeatherForecastDrinks.Services;
using Microsoft.OpenApi.Models;

// Moved top-level statements outside the namespace
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample Backend API", Version = "v1" });
});

// Log the BaseAddress during application startup
Console.WriteLine($"Configured CoffeeApiBaseUrl: {builder.Configuration["ApiSettings:CoffeeApiBaseUrl"]}");

// Register WeatherForecastService
builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();

// DEMO 1 - fix service registration
// ...

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample Backend API v1");
    });
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", (IWeatherForecastService weatherService) =>
{
    return weatherService.GetWeatherForecasts();
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// DEMO 2 - Fix API route
// ...

app.Run();
