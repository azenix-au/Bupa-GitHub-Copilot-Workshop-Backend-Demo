# How to Run the Project

1. **Clone the Repository**  
   Clone this repository to your local machine using the following command:
   ```bash
   git clone <repository-url>
   ```

2. **Navigate to the Project Directory**  
   Change into the project directory:
   ```bash
   cd WeatherForecastDrinks
   ```

3. **Restore Dependencies**  
   Restore the required NuGet packages:
   ```bash
   dotnet restore
   ```

4. **Set Up Configuration**  
   Ensure that the `appsettings.json` file contains the necessary configuration, such as the `ApiSettings:CoffeeApiBaseUrl`. If this is missing, add it to the file:
   ```json
   {
       "ApiSettings": {
           "CoffeeApiBaseUrl": "https://example.com/api"
       }
   }
   ```

5. **Run the Application**  
   Start the application using the following command:
   ```bash
   dotnet run
   ```

6. **Access the API**  
   Once the application is running, you can access the API at `http://localhost:5134`. Use tools like Postman or `curl` to interact with the endpoints.