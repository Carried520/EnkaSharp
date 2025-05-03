using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Mappers;

namespace EnkaSharp.Entities.Genshin.Abstractions;

public class PlayerInfo
{

    public string? Uid { get; set; }
    public int Level { get; set; }
    public string? Nickname { get; set; }
    public string? Signature { get; set; }
    public Uri? NameCardIconUri { get; set; }
    public int WorldLevel { get; set; }

    public int AchievementCount { get; set; }

    public int TowerFloorIndex { get; set; }
    public int TowerLevelIndex { get; set; }

    public AvatarInfoListItem[] ShowAvatarInfoList { get; set; } = [];
    public string?[] ShowNameCardIdUris { get; set; } = [];
    public ProfilePictureData? ProfilePicture { get; set; }
    public int FetterCount { get; set; }
}