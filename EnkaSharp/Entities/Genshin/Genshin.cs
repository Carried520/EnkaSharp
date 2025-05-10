using EnkaSharp.Entities.Genshin.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Retry;

namespace EnkaSharp.Entities.Genshin;

/// <summary>
/// Provides general abstraction for Genshin Enka API requests.
/// </summary>
public class Genshin
{
    private readonly IMemoryCache _cache;
    private readonly HttpClient _httpClient;

    private readonly ResiliencePipeline _genshinResiliencePipeline = new ResiliencePipelineBuilder()
        .AddRetry(new RetryStrategyOptions
            { Delay = EnkaClient.Config.RetryDelay, MaxRetryAttempts = EnkaClient.Config.RetryCount })
        .Build();


    public Genshin(IMemoryCache cache, HttpClient httpClient)
    {
        _cache = cache;
        _httpClient = httpClient;
    }


    /// <summary>
    /// Returns full genshin data of enka user.
    /// </summary>
    /// <param name="uid">Uid of user to be searched.</param>
    /// <param name="cancellationToken">Cancellation token to handle cancellation.</param>
    /// <returns><see cref="EnkaGenshinData"/></returns>
    /// <exception cref="InvalidOperationException">In case <see cref="EnkaGenshinData"/> is null.</exception>
    /// <exception cref="OperationCanceledException">If cancellation is requested with <see cref="CancellationToken"/>.</exception>
    /// <exception cref="AggregateException">When retries have failed - gathers all exceptions from failed attempts.</exception>
    public async Task<EnkaGenshinData> GetGenshinDataAsync(long uid, CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue($"enka-user-uid-{uid}", out EnkaGenshinData? user))
        {
            return user ?? throw new InvalidOperationException();
        }

        cancellationToken.ThrowIfCancellationRequested();

        RestGenshinData newRestGenshinData = await _genshinResiliencePipeline.ExecuteAsync(
            async token => await RestGenshinData.GetUserAsync(_httpClient, uid, token), cancellationToken); 

        EnkaGenshinData enkaGenshinData = newRestGenshinData.ToGenshinData();
        _cache.Set($"enka-user-uid-{uid}", enkaGenshinData, TimeSpan.FromMinutes(5));
        return enkaGenshinData;
    }

    /// <summary>
    /// Gets only <see cref="PlayerInfo"/> from enka.network API.
    /// </summary>
    /// <param name="uid">Uid of user to be searched.</param>
    /// <param name="cancellationToken">Cancellation token to handle cancellation.</param>
    /// <returns><see cref="EnkaGenshinInfo"/></returns>
    /// <exception cref="InvalidOperationException">In case <see cref="EnkaGenshinInfo"/> is null.</exception>
    /// <exception cref="OperationCanceledException">If cancellation is requested with <see cref="CancellationToken"/>.</exception>
    /// <exception cref="AggregateException">When retries have failed - gathers all exceptions from failed attempts.</exception>
    public async Task<EnkaGenshinInfo> GetGenshinInfoAsync(long uid, CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue($"enka-userinfo-uid-{uid}", out EnkaGenshinInfo? userInfo))
        {
            return userInfo ?? throw new InvalidOperationException();
        }

        cancellationToken.ThrowIfCancellationRequested();

        EnkaGenshinInfo newUserInfo = await _genshinResiliencePipeline.ExecuteAsync(
            async token => await EnkaGenshinInfo.GetEnkaInfo(_httpClient, uid, token), cancellationToken);

        _cache.Set($"enka-userinfo-uid-{uid}", newUserInfo, TimeSpan.FromMinutes(5));
        return newUserInfo;
    }
}