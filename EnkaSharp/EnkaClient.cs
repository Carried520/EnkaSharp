using System.Net;
using System.Text.Json;
using EnkaSharp.Entities.Base;
using Microsoft.Extensions.Caching.Memory;

namespace EnkaSharp;

/// <summary>
/// Base client type to serve as wrapper around Enka API.
/// It shouldn't be manually instantiated!
/// </summary>
public sealed class EnkaClient : IEnkaClient
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


    /// <summary>
    /// Handles custom errors given by Enka API.
    /// </summary>
    /// <param name="statusCode">Status code given by HTTP request.</param>
    /// <exception cref="HttpRequestException">Thrown when function hits specific HTTP Code.</exception>
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public User User { get; }

    public EnkaClient ShallowCopy()
    {
        return (EnkaClient)MemberwiseClone();
    }
}