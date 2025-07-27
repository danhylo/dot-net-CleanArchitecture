namespace ApiSample01.Application.Common.Base;

using System.Text.Json.Serialization;

public class Error
{
    [JsonPropertyOrder(1)]
    public string Code { get; set; } = string.Empty;
    
    [JsonPropertyOrder(2)]
    public string Message { get; set; } = string.Empty;
    
    [JsonPropertyOrder(3)]
    public string Application { get; set; } = string.Empty;
}