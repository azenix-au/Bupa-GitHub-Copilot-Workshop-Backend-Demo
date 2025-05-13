# WeatherForecastDrinks Tests

This project contains unit tests for the WeatherForecastDrinks application. The tests are designed to verify the functionality of the services within the application, ensuring that they behave as expected under various conditions.

## Project Structure

- **Services**
  - `CoffeeServiceTests.cs`: Contains unit tests for the `CoffeeService` class.
  - `WeatherForecastServiceTests.cs`: Contains unit tests for the `WeatherForecastService` class.

## Running the Tests

To run the tests in this project, follow these steps:

1. Ensure you have the .NET SDK installed on your machine.
2. Open a terminal and navigate to the `weather-forecast-drinks-tests` directory.
3. Run the following command to execute the tests:

   ```
   dotnet test
   ```

This command will build the project and run all the tests, providing you with a summary of the results.

## Additional Information

- Make sure that the main backend application is running if your tests depend on it.
- You can add more tests as needed to cover additional scenarios or edge cases for the services.