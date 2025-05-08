using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Entities.Genshin.Abstractions;
using EnkaSharp.Utils;
using EnkaSharp.Utils.Genshin;

namespace EnkaSharp.Mappers;

internal static class PlayerInfoMapper
{
    internal static PlayerInfo MapPlayerInfo(RestPlayerInfo restPlayerInfo, string? uid)
    {
        return new PlayerInfo
        {
            Uid = uid,
            Nickname = restPlayerInfo.Nickname,
            Level = restPlayerInfo.Level,
            Signature = restPlayerInfo.Signature,
            IconUri = GetPfpUri(restPlayerInfo.ProfilePicture.AvatarId),
            WorldLevel = restPlayerInfo.WorldLevel,
            AchievementCount = restPlayerInfo.AchievementCount,
            Abyss = new AbyssInfo
                { Floor = restPlayerInfo.TowerFloorIndex, Chamber = restPlayerInfo.TowerLevelIndex },
            Theater = new TheaterInfo
            {
                TheaterActIndex = restPlayerInfo.TheaterActIndex,
                TheaterStarIndex = restPlayerInfo.TheaterStarIndex
            },
            ShowcaseNameCardUris = MapShowNameCardIdList(restPlayerInfo.ShowNameCardIdList),
            NameCardIconUri = MapNameCardId(restPlayerInfo.NameCardId)
        };
    }


    private static Uri? GetPfpUri(int characterId)
    {
        var handler = EnkaClient.GetAssets<GenshinAssetHandler>(GameType.Genshin);


        if (handler.Data.ProfilePictures == null ||
            !handler.Data.ProfilePictures.TryGetValue(characterId.ToString(), out ProfilePicture? profilePicture))
            return ProfilePictureUtils.TryGetCharacterIconUri(characterId, out Uri? uri) ? uri : null;

        if (string.IsNullOrEmpty(profilePicture.IconPath))
            return ProfilePictureUtils.TryGetCharacterIconUri(characterId, out Uri? pfpUri) ? pfpUri : null;

        string path = profilePicture.IconPath.Replace("_Circle", "");
        return UriConstants.GetAssetUri(path);
    }

    private static Uri?[] MapShowNameCardIdList(int[] nameCardIds)
    {
        var genshinAssetHandler = EnkaClient.GetAssets<GenshinAssetHandler>(GameType.Genshin);
        Dictionary<string, NameCard>? namecards = genshinAssetHandler.Data.NameCards;
        return nameCardIds.Select(item => namecards?[item.ToString()].Icon).Select(UriConstants.GetAssetUri).ToArray();
    }

    private static Uri? MapNameCardId(int nameCardId)
    {
        IAssetHandler handler = EnkaClient.Assets[GameType.Genshin];
        if (handler is not GenshinAssetHandler genshinAssetHandler)
            throw new InvalidCastException("Wrong handler configured for Genshin.");

        Dictionary<string, NameCard>? namecards = genshinAssetHandler.Data.NameCards;
        string? icon = namecards?[nameCardId.ToString()].Icon;
        if (icon == null) return null;
        Uri iconUri = UriConstants.GetAssetUri(icon);
        return iconUri;
    }
}

public class AvatarInfoListItem
{
    public string? Name { get; set; }
    public int Level { get; set; }
    public EnergyType EnergyType { get; set; }
}

public class AbyssInfo
{
    public int Floor { get; set; }
    public int Chamber { get; set; }
}

public enum EnergyType
{
    Pyro = 1,
    Hydro = 2,
    Dendro = 3,
    Electro = 4,
    Cryo = 5,
    Anemo = 7,
    Geo = 8
}