namespace ApiSample01.Domain.Entities.Common.Api.Base;

using System.Text.Json.Serialization;

public class ApiResponsePage<T> : ApiResponse<T>
{
    [JsonPropertyOrder(5)]
    public Page Page { get; set; } = new();
}