namespace ApiSample01.Application.Common.Api.Base;

using ApiSample01.Application.Common.Base;

using System.Text.Json.Serialization;

public class ApiErrorResponse
{
    [JsonPropertyOrder(1)]
    public int HttpCode { get; set; }
    
    [JsonPropertyOrder(2)]
    public string HttpMessage { get; set; } = string.Empty;
    
    [JsonPropertyOrder(3)]
    public bool Status { get; set; }
    
    [JsonPropertyOrder(4)]
    public Error Error { get; set; } = new();
    
    [JsonPropertyOrder(5)]
    public Transaction Transaction { get; set; } = new();
}