# EnkaSharp
## About
A wrapper over enka.network API , supporting Dependency Injection and MemoryCache.

## License
This project is open source project , licensed under MIT license. 
Please abide by license terms. You must provide MIT license in copied/derived in parts that contain / derive from this code.

## 📦 Semantic Versioning (SemVer)
This project follows Semantic Versioning (SemVer), which uses a version format of `MAJOR.MINOR.PATCH`.

- **Patch** — Increases when bug fixes, new features, new methods or small improvements are made that do not affect compatibility.
- **Minor** — Increases when new features or functionality are added in a backward-compatible manner.
- **Major** — Increases when there are incompatible changes that break backward compatibility.

See more : [Microsoft Versioning Docs](https://learn.microsoft.com/en-us/dotnet/csharp/versioning#authoring-libraries)
## Branches
### Main 
Branch with release ready code.
### Dev 
Developement branch. Pull Requests are targetted here.
### Feature/X 
Feature branch that targets dev. Aims to introduce new feature.
### Bugfix/X 
Branch that aims to fix bugs. Targets dev.
### Docs/X 
Branch that adds new documentation. Targets dev.

## Installation (Nuget)
[EnkaSharp](https://www.nuget.org/packages/EnkaSharp)
## Game support

| Game              | Status            |
|-------------------|-------------------|
| Genshin Impact    | ✅ Ready           | 
| Honkai: Star Rail | ✖ Not Implemented |  
| Zenless Zone Zero | ✖ Not Implemented |

## Creating Client
```csharp
var memoryCache = new MemoryCache(new MemoryCacheOptions());
IEnkaClient client = EnkaProviderFactory.Create(new EnkaClientConfig { UserAgent = "Carried-Api-Test"} , memoryCache);
// we have to call InitializeAsync() to populate assets before we can use EnkaClient
await client.InitializeAsync();
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
