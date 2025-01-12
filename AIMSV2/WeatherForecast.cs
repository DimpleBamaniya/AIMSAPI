namespace AIMSV2
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}

// "DefaultConnection": "Server=SDS-PC-093\\SQLEXPRESS;Database=AIMSV3;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;Connection Timeout=30;"
