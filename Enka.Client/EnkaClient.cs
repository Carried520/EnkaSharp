using System.Net;
using System.Text.Json;
using Enka.Client.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Enka.Client;

public class EnkaClient : IEnkaClient
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;

    private readonly JsonSerializerOptions _jsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };


    public EnkaClient(HttpClient httpClient, string userAgent, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
        _httpClient.BaseAddress = new Uri("https://enka.network/api/");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
        
        User = new User(_cache, _jsonSerializerOptions, _httpClient);
    }


    internal static void HandleError(HttpStatusCode statusCode)
    {
        string? errorResponse = statusCode switch
        {
            HttpStatusCode.BadRequest => "Invalid UID Format",
            HttpStatusCode.NotFound => "Player does not exist",
            HttpStatusCode.FailedDependency => "Game maintenance / everything is broken after the game update",
            HttpStatusCode.TooManyRequests => "Rate-limited (either by my server or by MHY server)",
            HttpStatusCode.InternalServerError => "Internal Server Error",
            HttpStatusCode.ServiceUnavailable => "Enka's dev fault",
            _ => null
        };

        if (!string.IsNullOrEmpty(errorResponse))
            throw new HttpRequestException($"{errorResponse}");
    }

    public User User { get; }
}