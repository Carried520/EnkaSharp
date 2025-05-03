using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Base.Abstractions;
using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Entities.Genshin.Abstractions;
using EnkaSharp.Utils;

namespace EnkaSharp.Mappers;

internal static class PlayerInfoMapper
{
    internal static PlayerInfo MapPlayerInfo(RestPlayerInfo restPlayerInfo , string? uid)
    {
        return new PlayerInfo
        {
            Uid = uid,
            Level = restPlayerInfo.Level,
            Nickname = restPlayerInfo.Nickname,
            Signature = restPlayerInfo.Signature,
            NameCardIconUri = MapNameCardId(restPlayerInfo.NameCardId),
            WorldLevel = restPlayerInfo.WorldLevel,
            AchievementCount = restPlayerInfo.AchievementCount,
            TowerFloorIndex = restPlayerInfo.TowerFloorIndex,
            TowerLevelIndex = restPlayerInfo.TowerLevelIndex,
            ShowAvatarInfoList = MapShowAvatarInfoList(restPlayerInfo.ShowAvatarInfoList),
            ShowNameCardIdUris = MapShowNameCardIdList(restPlayerInfo.ShowNameCardIdList),
            ProfilePicture = restPlayerInfo.ProfilePicture,
            FetterCount = restPlayerInfo.FetterCount,
        };
    }

    
   
    private static string?[] MapShowNameCardIdList(int[] nameCardIds)
    {
        IAssetHandler handler = EnkaClient.Assets[GameType.Genshin];
        if (handler is not GenshinAssetHandler genshinAssetHandler)
            throw new InvalidCastException("Wrong handler configured for Genshin.");
        
        Dictionary<string, NameCard>? namecards = genshinAssetHandler.Data.NameCards;
        return nameCardIds.Select(item => namecards?[item.ToString()].Icon).ToArray();

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

    private static AvatarInfoListItem[] MapShowAvatarInfoList(AvatarInfoListNode[] avatarInfoListNodes)
    {
        IAssetHandler handler = EnkaClient.Assets[GameType.Genshin];
        if (handler is not GenshinAssetHandler genshinAssetHandler)
            throw new InvalidCastException("Wrong handler configured for Genshin.");


        return avatarInfoListNodes.Where(node => Enum.IsDefined(typeof(EnergyType), node.EnergyType))
            .Select(node =>
            {
                CharacterData characterData = genshinAssetHandler.Data.Characters?[node.AvatarId.ToString()] ??
                                              throw new
                                                  InvalidOperationException();
                string name = genshinAssetHandler.Data.Localization?[EnkaClient.Config.Language][characterData.NameTextMapHash.ToString()] ??
                              throw new InvalidOperationException();

                return new AvatarInfoListItem
                {
                    Name = name, EnergyType = (EnergyType)node.EnergyType, Level = node.Level
                };
            }).ToArray();
    }
}

public class AvatarInfoListItem
{
    public string? Name { get; set; }
    public int Level { get; set; }
    public EnergyType EnergyType { get; set; }
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