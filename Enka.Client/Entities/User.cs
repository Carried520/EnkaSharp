using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace Enka.Client.Entities;

public class User
{
    private readonly IMemoryCache _cache;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public User(IMemoryCache cache, JsonSerializerOptions jsonSerializerOptions, HttpClient httpClient)
    {
        _cache = cache;
        _jsonSerializerOptions = jsonSerializerOptions;
        _httpClient = httpClient;
    }

    public async Task<EnkaUser> GetEnkaUserAsync(string name)
    {
        if (_cache.TryGetValue($"enka-user-{name}", out EnkaUser? user))
        {
            return user ?? throw new InvalidOperationException();
        }

        EnkaUser newUser = await RequestUserAsync(name);
        _cache.Set($"enka-user-{name}", newUser, TimeSpan.FromMinutes(5));
        return newUser;
    }


    private async Task<EnkaUser> RequestUserAsync(string name)
    {
        HttpResponseMessage request = await _httpClient.GetAsync($"profile/{name}/hoyos/");
        if (!request.IsSuccessStatusCode)
            EnkaClient.HandleError(request.StatusCode);

        await using Stream responseStream = await request.Content.ReadAsStreamAsync();
        Dictionary<string, JsonElement>? deserialized =
            await JsonSerializer.DeserializeAsync<Dictionary<string, JsonElement>>(responseStream,
                _jsonSerializerOptions);

        return deserialized?.First().Value.Deserialize<EnkaUser>(_jsonSerializerOptions) ??
               throw new InvalidOperationException("Could not parse requested user");
    }
}