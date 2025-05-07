# EnkaSharp
## About
A wrapper over enka.network API , supporting Dependency Injection and MemoryCache.
## Installation (Nuget)
[EnkaSharp](https://www.nuget.org/packages/EnkaSharp)
## Game support

| Game              | Status        |
| ----------------- | -------------- 
| Genshin Impact    | ✅ Ready | 
| Honkai: Star Rail | ✖ Not Implemented|  
| Zenless Zone Zero |  ✖ Not Implemented |

## Creating Client
```csharp
var memoryCache = new MemoryCache(new MemoryCacheOptions());
IEnkaClient client = EnkaProviderFactory.Create(new EnkaClientConfig { UserAgent = "Carried-Api-Test"} , memoryCache);
// we have to call InitializeAsync() to populate assets before we can use EnkaClient
await _client.InitializeAsync();
```

For Asp.Net and other Dependency Injection Scenarios:
```csharp
builder.Services.AddMemoryCache();
builder.Services.AddEnkaClient(new EnkaClientConfig { UserAgent = "Carried-Api-Test"});
```
For ASP.NET best bet would be to create BackgroundService to initialize our assets:
```csharp
builder.Services.AddHostedService<StartupService>();
```

```csharp
public class StartupService : BackgroundService
{
    private readonly IEnkaClient _client;

    public StartupService(IEnkaClient client)
    {
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _client.InitializeAsync();
    }
}
```

## Using Client
```csharp
// provide uid - your hoyoverse uid
EnkaGenshinData genshinData = await _enkaClient.Genshin.GetGenshinDataAsync(uid);
// access properties freely
```
