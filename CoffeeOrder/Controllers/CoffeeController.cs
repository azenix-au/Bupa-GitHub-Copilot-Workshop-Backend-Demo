using CoffeeOrder.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeOrder.Controllers;

[ApiController]
[Route("[controller]")]
public class CoffeeController : ControllerBase
{
    private readonly ICoffeeService _coffeeService;

    public CoffeeController(ICoffeeService coffeeService)
    {
        _coffeeService = coffeeService;
    }

    [HttpGet]
    public List<Coffee> GetCoffee()
    {
        var coffeeData = _coffeeService.GetCoffeeData();
        return coffeeData;
    }
}