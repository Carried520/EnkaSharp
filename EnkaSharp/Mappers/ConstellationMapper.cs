using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Genshin.Abstractions;
using EnkaSharp.Utils;

namespace EnkaSharp.Mappers;

internal static class ConstellationMapper
{
    internal static Constellation[] MapConstellations(int[] constellationIds)
    {
        return constellationIds.Select((constellationId, index) => new Constellation
        {
            Name = GetConstellationName(constellationId),
            IconUri = GetConstellationUri(constellationId),
            Rank = index + 1
        }).ToArray();
    }

    private static Uri? GetConstellationUri(int id)
    {
        var handler = EnkaClient.GetAssets<GenshinAssetHandler>(GameType.Genshin);
        if (handler.Data.Constellations == null)
            throw new InvalidOperationException("Constellations were null!");

        return handler.Data.Constellations.TryGetValue(id.ToString(), out ConstellationData? constellationData)
            ? UriConstants.GetAssetUri(constellationData.Icon)
            : null;
    }
    private static string? GetConstellationName(int id)
    {
        var handler = EnkaClient.GetAssets<GenshinAssetHandler>(GameType.Genshin);
        if (handler.Data.Constellations == null)
            throw new InvalidOperationException("Constellations were null!");

        return handler.Data.Constellations.TryGetValue(id.ToString(), out ConstellationData? constellationData)
            ? handler.Data.TextMap?[EnkaClient.Config.Language][constellationData.NameTextMapHash.ToString()]
            : $"Unknown_Constellation_{id}";
    }
}