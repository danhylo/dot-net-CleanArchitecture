namespace ApiSample01.Application.Common.Api.Base;

using ApiSample01.Application.Common.Base;

using System.Text.Json.Serialization;

public class ApiResponsePage<T> : ApiResponse<T>
{
    [JsonPropertyOrder(5)]
    public Page Page { get; set; } = new();
}