using System.Text.Json;
using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Mappers;

namespace EnkaSharp.Entities.Base.Abstractions;

public class EnkaRestUser : IEnkaUser
{
    public RestPlayerInfo? PlayerInfo { get; set; }
    public RestAvatarInfo[] AvatarInfoList { get; set; } = [];
    public int Ttl { get; set; }
    public string? Uid { get; set; }
    public Owner? Owner { get; set; }

    internal static async Task<EnkaRestUser> GetUserAsync(HttpClient client, long uid)
    {
        HttpResponseMessage request = await client.GetAsync($"uid/{uid}");
        if (!request.IsSuccessStatusCode)
            EnkaClient.HandleError(request.StatusCode);

        await using Stream responseStream = await request.Content.ReadAsStreamAsync();
        var user = await JsonSerializer.DeserializeAsync<EnkaRestUser>(responseStream,
            JsonSettings.CamelCase);
        return user ?? throw new InvalidOperationException();
    }

    internal EnkaUser ToUser()
    {
        Dictionary<PropMapNodeType, int>[] propMaps =
            AvatarInfoList.Select(info => PropMapMapper.MapPropMap(info.PropMap)).ToArray();
        Dictionary<PropMapNodeType, int>? firstPropMap = propMaps.FirstOrDefault();


        if (EnkaClient.Assets[GameType.Genshin] is not GenshinAssetHandler genshinAssets)
            throw new InvalidCastException("Error when getting genshin handler");
        if (genshinAssets.Data.Localization is null || genshinAssets.Data.Characters is null)
            throw new InvalidOperationException("Error getting character data");


        var user = new EnkaUser(PlayerInfo, AvatarInfoList, Ttl, Uid, Owner)
        {
            Characters = AvatarInfoList.Select(info =>
            {
                Dictionary<PropMapNodeType, int> propMap = PropMapMapper.MapPropMap(info.PropMap);
                Dictionary<FightPropType, double> battleMap = PropMapMapper.MapFightProps(info.FightPropMap);
                CharacterData? textHash = genshinAssets.Data.Characters?[info.AvatarId.ToString()];
                string? name =
                    genshinAssets.Data.Localization?["en"][
                        textHash?.NameTextMapHash.ToString() ?? throw new InvalidOperationException()];
                return new Character(name ?? throw new InvalidOperationException(), propMap, battleMap);
            }).ToArray(),
            Stamina = firstPropMap is null ? 0 : firstPropMap[PropMapNodeType.Stamina] / 100
        };

        return user;
    }
}