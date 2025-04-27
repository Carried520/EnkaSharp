using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace EnkaSharp;

/// <summary>
/// Provides extensions to help register EnkaClient.
/// </summary>
public static class EnkaDependencyInjectionExtensions
{
    /// <summary>
    /// Adds EnkaClient to Dependency Injection Container.
    /// Note : Requires MemoryCache to be added to Container as well.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>
    /// <param name="userAgent">User Agent to use for HTTP requests.</param>
    /// <returns>An <see cref="IServiceCollection"/> to configure more services. </returns>
    public static IServiceCollection AddEnkaClient(this IServiceCollection serviceCollection, string userAgent)
    {
        serviceCollection.AddHttpClient<EnkaClient>()
            .AddTypedClient<IEnkaClient>((client, sp) =>
            {
                IMemoryCache cache = sp.GetRequiredService<IMemoryCache>();
                return new EnkaClient(client, userAgent , cache);
            });
        return serviceCollection;
    }
}