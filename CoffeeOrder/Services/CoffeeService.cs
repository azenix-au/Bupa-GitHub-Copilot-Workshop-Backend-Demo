namespace CoffeeOrder.Services;
public class CoffeeService : ICoffeeService
{
    public List<Coffee> GetCoffeeData()
    {
        return new List<Coffee>
        {
            new Coffee("Espresso", "Strong and bold coffee.", new List<string> { "Water", "Coffee beans" }, "Hot", "1", "Mild"),
            new Coffee("Latte", "Creamy coffee with milk.", new List<string> { "Milk", "Coffee beans" }, "Hot", "2", "Cool"),
            new Coffee("Iced Coffee", "Chilled coffee with ice.", new List<string> { "Water", "Coffee beans", "Ice" }, "Cold", "3", "Hot")
        };
    }
}