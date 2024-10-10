internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpClient("externalweatherapi", c => c.BaseAddress = new Uri("http://localhost:5085"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapGet("/weatherforecast", () =>
        {
            //use the httpclient to call the external api
            var client = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("externalweatherapi");
            var response = client.GetAsync("/weatherforecast").Result;
            var forecast = response.Content.ReadFromJsonAsync<WeatherForecast[]>().Result;

            for (int i = 0; i < forecast.Length; i++)
            {
                forecast[i] = forecast[i] with { Summary2 = GetSummary(forecast[i].TemperatureC) };
            }
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        app.Run();
    }


    public static string[] Summaries = {"Arktisch", "Eisig", "Kalt", "Kühl", "Mild", "Warm", "Lau", "Heiß", "Glühend", "Sengend"};
    static string GetSummary(int temperatureC)
    {
        if (temperatureC <= -10)
        {
            return Summaries[0];
        }
        else if (temperatureC > -10 && temperatureC <= 0)
        {
            return Summaries[1];
        }
        else if (temperatureC > 5 && temperatureC <= 10)
        {
            return Summaries[2];
        }
        else if (temperatureC > 10 && temperatureC <= 15)
        {
            return Summaries[3];
        }
        else if (temperatureC > 15 && temperatureC <= 20)
        {
            return Summaries[4];
        }
        else if (temperatureC > 20 && temperatureC <= 25)
        {
            return Summaries[5];
        }
        else if (temperatureC > 25 && temperatureC <= 30)
        {
            return Summaries[6];
        }
        else if (temperatureC > 30 && temperatureC <= 35)
        {
            return Summaries[7];
        }
        else if (temperatureC > 35 && temperatureC <= 40)
        {
            return Summaries[8];
        }
        else
        {
            return Summaries[9];
        }
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary, string? Summary2)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
