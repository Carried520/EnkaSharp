using System.Text.Json;
using EnkaSharp.Entities.Base.Abstractions;
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
    


    public async Task<EnkaGenshinData> GetUserAsync(long uid)
    {
        if (_cache.TryGetValue($"enka-user-uid-{uid}", out EnkaGenshinData? user))
        {
            return user ?? throw new InvalidOperationException();
        }

        RestGenshinData newRestGenshinData = await RestGenshinData.GetUserAsync(_httpClient, uid);
        EnkaGenshinData enkaGenshinData = newRestGenshinData.ToGenshinData();
        _cache.Set($"enka-user-uid-{uid}", enkaGenshinData, TimeSpan.FromMinutes(5));
        return enkaGenshinData;
    }

    public async Task<EnkaInfo> GetUserInfoAsync(long uid)
    {
        if (_cache.TryGetValue($"enka-userinfo-uid-{uid}", out EnkaInfo? userInfo))
        {
            return userInfo ?? throw new InvalidOperationException();
        }

        var newUserInfo = await EnkaInfo.GetEnkaInfo(_httpClient, uid);
        _cache.Set($"enka-userinfo-uid-{uid}", newUserInfo, TimeSpan.FromMinutes(5));
        return newUserInfo;
    }
}