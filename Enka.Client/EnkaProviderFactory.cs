using Microsoft.Extensions.Caching.Memory;

namespace Enka.Client;

public static class EnkaProviderFactory
{
    private static readonly HttpClient SharedClient = new();

    public static IEnkaClient Create(string userAgent , IMemoryCache cacheProvider) => new EnkaClient(SharedClient, userAgent , cacheProvider);
}