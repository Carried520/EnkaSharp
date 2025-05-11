using EnkaSharp.AssetHandlers;

namespace EnkaSharp.Utils;

internal static class UriConstants
{
    private const string GenshinBaseAssetUri = "https://enka.network/ui/";
    private const string HsrBaseAssetUri = "https://enka.network/api/hsr/";
    internal static Uri GetAssetUri(string? resourceName, GameType gameType)
    {
        string baseUri =  gameType switch
        {
            GameType.Genshin => GenshinBaseAssetUri,
            GameType.Hsr => HsrBaseAssetUri,
            GameType.Zzz => throw new NotImplementedException("ZZZ is not implemented yet"),
            _ => throw new ArgumentOutOfRangeException(nameof(gameType), gameType, null)
        };

        return new Uri($"{baseUri}{resourceName}.png");
    }
}