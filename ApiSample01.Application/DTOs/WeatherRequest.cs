namespace ApiSample01.Application.DTOs;

public class WeatherRequest
{
    public int Days { get; set; }
    public int Start { get; set; }
    public int Limit { get; set; }
}