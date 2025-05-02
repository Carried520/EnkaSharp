using System.Net;
using System.Text.Json;
using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Base;
using EnkaSharp.Exceptions;
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
    internal static void HandleError(HttpStatusCode statusCode)
    {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch ((int)statusCode)
        {
            case 400:
                throw new InvalidUidException();
            case 404:
                throw new PlayerNotFoundException();
            case 424:
                throw new ApiBrokenException();
            case 429:
                throw new RateLimitException();
            case 500:
                throw new InternalServerErrorException();
            case 503:
                throw new ApiBrokenException();
            default:
                return;
        }
    }

    public async Task InitializeAsync()
    {
        await Assets.InitAsync();
        IsInitialized = true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public User User { get; }

    internal bool IsInitialized { get; set; } = false;
    internal static AssetDispatcher Assets { get; set; } = new();

    public EnkaClient ShallowCopy()
    {
        return (EnkaClient)MemberwiseClone();
    }

    public GenshinAssetData GetAssets()
    {
        IAssetHandler handler = Assets[GameType.Genshin];
        if (handler is not GenshinAssetHandler genshinAssetHandler)
            throw new InvalidOperationException();
        return genshinAssetHandler.Data;
    }
}