namespace ApiSample01.Domain.Entities.Common.Api.Base;

using System.Text.Json.Serialization;

public class ApiResponse<T>
{
    [JsonPropertyOrder(1)]
    public int HttpCode { get; set; }
    
    [JsonPropertyOrder(2)]
    public string HttpMessage { get; set; } = string.Empty;
    
    [JsonPropertyOrder(3)]
    public bool Status { get; set; }
    
    [JsonPropertyOrder(4)]
    public T? Data { get; set; }
    
    [JsonPropertyOrder(6)]
    public Transaction Transaction { get; set; } = new();
}