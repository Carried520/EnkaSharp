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
    /// <param name="config"><see cref="EnkaClientConfig"/></param>
    /// <returns>An <see cref="IServiceCollection"/> to configure more services. </returns>
    public static IServiceCollection AddEnkaClient(this IServiceCollection serviceCollection, EnkaClientConfig config)
    {
        serviceCollection.AddHttpClient<EnkaClient>()
            .AddTypedClient<IEnkaClient>((client, sp) =>
            {
                var cache = sp.GetRequiredService<IMemoryCache>();
                return new EnkaClient(client, config , cache);
            });
        return serviceCollection;
    }
}