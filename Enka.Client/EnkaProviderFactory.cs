using Microsoft.Extensions.Caching.Memory;

namespace Enka.Client;

/// <summary>
/// EnkaClient provider for Applications that don't use Dependency Injection.
/// </summary>
public static class EnkaProviderFactory
{
    /// <summary>
    /// Shared <see cref="HttpClient"/> to avoid socket starvation.
    /// </summary>
    private static readonly HttpClient SharedClient = new();

    /// <summary>
    /// Creates an instance of IEnkaClient.
    /// </summary>
    /// <param name="userAgent">User Agent to use for HTTP requests.</param>
    /// <param name="cacheProvider">Instance of IMemoryCache to be passed as caching provider.</param>
    /// <returns> <see cref="IEnkaClient"/> </returns>
    public static IEnkaClient Create(string userAgent , IMemoryCache cacheProvider) => new EnkaClient(SharedClient, userAgent , cacheProvider);
}