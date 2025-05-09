using System.Net;
using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Genshin;
using EnkaSharp.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace EnkaSharp;

/// <summary>
/// Base client type to serve as wrapper around Enka API.
/// It shouldn't be manually instantiated!
/// </summary>
public sealed class EnkaClient : IEnkaClient
{
    public EnkaClient(HttpClient httpClient, EnkaClientConfig clientConfig, IMemoryCache cache)
    {
        httpClient.BaseAddress = new Uri("https://enka.network/api/");
        httpClient.DefaultRequestHeaders.Add("User-Agent", clientConfig.UserAgent);

        Genshin = new Genshin(cache, httpClient);
        Config = clientConfig;
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
                throw new InvalidUidException("Uid was in invalid format");
            case 404:
                throw new PlayerNotFoundException("Player not found");
            case 424:
                throw new ApiBrokenException("Game maintenance / Api is broken after update");
            case 429:
                throw new RateLimitException("Rate limited");
            case 500:
                throw new InternalServerErrorException("Internal Server Error");
            case 503:
                throw new ApiBrokenException("Api is broken due to developer's fault");
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
    public Genshin Genshin { get; }

    internal static EnkaClientConfig Config { get; private set; } = new();

    internal static bool IsInitialized { get; private set; }
    internal static AssetDispatcher Assets { get; } = new();


    internal static T GetAssets<T>(GameType gameType) where T : IAssetHandler
    {
        IAssetHandler handler = Assets[gameType];
        if (handler is not T gameAssetHandler)
            throw new InvalidCastException("Wrong handler registered for the game");
        return gameAssetHandler;
    }

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