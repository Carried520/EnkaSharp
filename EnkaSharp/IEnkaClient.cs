using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities;
using EnkaSharp.Entities.Base;
using EnkaSharp.Entities.Genshin;

namespace EnkaSharp;

/// <summary>
/// Represents an abstraction for EnkaClient.
/// </summary>
public interface IEnkaClient
{
    /// <summary>
    /// Provides general abstraction for general Enka API User requests.
    /// </summary>
    public Genshin Genshin { get; }

    public Task InitializeAsync();
    public GenshinAssetData GetAssets();
}