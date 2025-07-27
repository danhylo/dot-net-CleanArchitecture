using System.ComponentModel.DataAnnotations;
using ApiSample01.Api.Attributes;

namespace ApiSample01.Api.Models;

public class WeatherForecastRequest
{
    [DaysValidation]
    public int Days { get; set; } = 2;

    [StartValidation]
    public int Start { get; set; } = 1;

    [LimitValidation]
    public int Limit { get; set; } = 100;
}