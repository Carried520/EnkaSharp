using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Enka.Client;

public static class EnkaDependencyInjectionExtensions
{
    public static IServiceCollection AddEnkaClient(this IServiceCollection serviceCollection, string userAgent)
    {
        serviceCollection.AddHttpClient<EnkaClient>()
            .AddTypedClient<IEnkaClient>((client, sp) =>
            {
                var cache = sp.GetRequiredService<IMemoryCache>();
                return new EnkaClient(client, userAgent , cache);
            });
        return serviceCollection;
    }
}