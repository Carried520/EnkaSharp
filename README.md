# Enka.Client

Creating client:
```csharp
var memoryCache = new MemoryCache(new MemoryCacheOptions());
IEnkaClient client = EnkaProviderFactory.Create("My-User-Agent" , memoryCache);
```

For Asp.Net and other Dependency Injection Scenarios:
```csharp
builder.Services.AddMemoryCache();
builder.Services.AddEnkaClient("My-User-Agent");
```
