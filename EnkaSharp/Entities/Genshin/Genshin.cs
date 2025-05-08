using System.Text.Json;
using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Entities.Genshin.Abstractions;
using EnkaSharp.Entities.Genshin.Raw;
using Microsoft.Extensions.Caching.Memory;

namespace EnkaSharp.Entities.Genshin;

/// <summary>
/// Provides general abstraction for Genshin Enka API requests.
/// </summary>
public class Genshin
{
    private readonly IMemoryCache _cache;
    private readonly HttpClient _httpClient;


    public Genshin(IMemoryCache cache, HttpClient httpClient)
    {
        _cache = cache;
        _httpClient = httpClient;
    }


    public async Task<EnkaGenshinData> GetGenshinDataAsync(long uid, CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue($"enka-user-uid-{uid}", out EnkaGenshinData? user))
        {
            return user ?? throw new InvalidOperationException();
        }

        cancellationToken.ThrowIfCancellationRequested();

        RestGenshinData newRestGenshinData = await RestGenshinData.GetUserAsync(_httpClient, uid, cancellationToken);
        EnkaGenshinData enkaGenshinData = newRestGenshinData.ToGenshinData();
        _cache.Set($"enka-user-uid-{uid}", enkaGenshinData, TimeSpan.FromMinutes(5));
        return enkaGenshinData;
    }

    public async Task<EnkaGenshinInfo> GetGenshinInfoAsync(long uid, CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue($"enka-userinfo-uid-{uid}", out EnkaGenshinInfo? userInfo))
        {
            return userInfo ?? throw new InvalidOperationException();
        }

        cancellationToken.ThrowIfCancellationRequested();

        EnkaGenshinInfo newUserInfo = await EnkaGenshinInfo.GetEnkaInfo(_httpClient, uid, cancellationToken);
        _cache.Set($"enka-userinfo-uid-{uid}", newUserInfo, TimeSpan.FromMinutes(5));
        return newUserInfo;
    }
}