using System.Text.Json;
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

        Snapshot newUser = await Snapshot.RequestSnapshotAsync(_httpClient, name);
        _cache.Set($"enka-user-{name}", newUser, TimeSpan.FromMinutes(5));
        return newUser;
    }


    public async Task<EnkaRestUser> GetUserAsync(long uid)
    {
        if (_cache.TryGetValue($"enka-user-uid-{uid}", out EnkaRestUser? user))
        {
            return user ?? throw new InvalidOperationException();
        }

        EnkaRestUser newRestUser = await EnkaRestUser.GetUserAsync(_httpClient, uid);
        _cache.Set($"enka-user-uid-{uid}", newRestUser, TimeSpan.FromMinutes(5));
        return newRestUser;
    }

    public async Task<EnkaInfo> GetUserInfoAsync(long uid)
    {
        if (_cache.TryGetValue($"enka-userinfo-uid-{uid}", out EnkaInfo? userInfo))
        {
            return userInfo ?? throw new InvalidOperationException();
        }

        EnkaInfo newUserInfo = await EnkaInfo.GetEnkaInfo(_httpClient, uid);
        _cache.Set($"enka-userinfo-uid-{uid}", newUserInfo, TimeSpan.FromMinutes(5));
        return newUserInfo;
    }
}