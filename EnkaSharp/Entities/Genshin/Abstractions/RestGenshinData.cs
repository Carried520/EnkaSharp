using System.Text.Json;
using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Entities.Genshin.Raw;
using EnkaSharp.Mappers;

namespace EnkaSharp.Entities.Genshin.Abstractions;

internal class RestGenshinData : IGenshinData
{
    internal RestPlayerInfo? PlayerInfo { get; set; }
    internal RestAvatarInfo[] AvatarInfoList { get; set; } = [];
    internal int Ttl { get; set; }
    internal string? Uid { get; set; }
    public Owner? Owner { get; set; }

    internal static async Task<RestGenshinData> GetUserAsync(HttpClient client, long uid,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage request = await client.GetAsync($"uid/{uid}", cancellationToken);
        if (!request.IsSuccessStatusCode)
            EnkaClient.HandleError(request.StatusCode);

        await using Stream responseStream = await request.Content.ReadAsStreamAsync(cancellationToken);
        var user = await JsonSerializer.DeserializeAsync<RestGenshinData>(responseStream,
            JsonSettings.CamelCase, cancellationToken);
        return user ?? throw new InvalidOperationException();
    }

    internal EnkaGenshinData ToGenshinData()
    {
        var genshinAssets = EnkaClient.GetAssets<GenshinAssetHandler>(GameType.Genshin);
        if (genshinAssets.Data.Localization is null || genshinAssets.Data.Characters is null)
            throw new InvalidOperationException("Error getting character data");


        var user = new EnkaGenshinData(PlayerInfo, Ttl, Uid, Owner)
        {
            Characters = AvatarInfoList.Select(AvatarInfoMapper.MapAvatarInfo).ToArray()
        };

        return user;
    }
}