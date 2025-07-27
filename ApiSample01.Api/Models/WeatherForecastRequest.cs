using System.ComponentModel.DataAnnotations;

namespace ApiSample01.Api.Models;

public class WeatherForecastRequest
{
    [Range(1, 100, ErrorMessage = "Days must be between 1 and 100")]
    public int Days { get; set; } = 2;

    [Range(1, int.MaxValue, ErrorMessage = "Start must be greater than 0")]
    public int Start { get; set; } = 1;

    [Range(1, 1000, ErrorMessage = "Limit must be between 1 and 1000")]
    public int Limit { get; set; } = 100;
}