using System.Text.Json;
using EnkaSharp.Entities.Base.Abstractions;
using EnkaSharp.Entities.Base.Raw;
using Microsoft.Extensions.Caching.Memory;

namespace EnkaSharp.Entities.Base;

/// <summary>
/// Provides general abstraction for general Enka API User requests.
/// </summary>
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

    /// <summary>
    /// Gets hoyos for user.
    /// </summary>
    /// <param name="name">Name of the user</param>
    /// <returns><see cref="Hoyos"/> representing Hoyos</returns>
    /// <exception cref="InvalidOperationException">Thrown when cache doesn't have user - shouldn't happen </exception>
    public async Task<Hoyos> GetHoyosAsync(string name)
    {
        if (_cache.TryGetValue($"enka-user-{name}", out Hoyos? user))
        {
            return user ?? throw new InvalidOperationException();
        }

        Hoyos newUser = await Hoyos.RequestUserAsync(_httpClient, _jsonSerializerOptions, name);
        _cache.Set($"enka-user-{name}", newUser, TimeSpan.FromMinutes(5));
        return newUser;
    }

    /// <summary>
    /// Gets snapshot.
    /// </summary>
    /// <param name="name">username</param>
    /// <returns><see cref="Snapshot"/> representing snapshot.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<Snapshot> GetSnapshotAsync(string name)
    {
        if (_cache.TryGetValue($"enka-snapshot-{name}", out Snapshot? user))
        {
            return user ?? throw new InvalidOperationException();
        }

        var newUser = await Snapshot.RequestSnapshotAsync(_httpClient, name);
        _cache.Set($"enka-user-{name}", newUser, TimeSpan.FromMinutes(5));
        return newUser;
    }


    public async Task<EnkaUser> GetUserAsync(long uid)
    {
        if (_cache.TryGetValue($"enka-user-uid-{uid}", out EnkaUser? user))
        {
            return user ?? throw new InvalidOperationException();
        }

        EnkaRestUser newRestUser = await EnkaRestUser.GetUserAsync(_httpClient, uid);
        EnkaUser enkaUser = newRestUser.ToUser();
        _cache.Set($"enka-user-uid-{uid}", enkaUser, TimeSpan.FromMinutes(5));
        return enkaUser;
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