using System.Text.Json.Serialization;
using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Mappers;

namespace EnkaSharp.Entities.Base.Abstractions;

public class PlayerInfo
{
    public int Level { get; set; }
    public string? Nickname { get; set; }
    public string? Signature { get; set; }
    public Uri? NameCardIconUri { get; set; }
    [JsonPropertyName("worldLevel")] public int WorldLevel { get; set; }

    [JsonPropertyName("finishAchievementNum")]
    public int AchievementCount { get; set; }

    public int TowerFloorIndex { get; set; }
    public int TowerLevelIndex { get; set; }

    public AvatarInfoListItem[] ShowAvatarInfoList { get; set; } = [];
    public string?[] ShowNameCardIdUris { get; set; } = [];
    public ProfilePictureData? ProfilePicture { get; set; }


    [JsonPropertyName("fetterCount")] public int FetterCount { get; set; }
}