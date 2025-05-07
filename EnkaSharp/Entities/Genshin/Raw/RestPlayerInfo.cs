using System.Text.Json.Serialization;

namespace EnkaSharp.Entities.Base.Raw;

/// <summary>
/// Represents PlayerInfo.
/// </summary>
public class RestPlayerInfo
{
    public int Level { get; set; }
    public string? Nickname { get; set; }
    public string? Signature { get; set; }
    [JsonPropertyName("nameCardId")] public int NameCardId { get; set; }
    [JsonPropertyName("worldLevel")] public int WorldLevel { get; set; }

    [JsonPropertyName("finishAchievementNum")]
    public int AchievementCount { get; set; }

    public int TowerFloorIndex { get; set; }
    public int TowerLevelIndex { get; set; }

    public AvatarInfoListNode[] ShowAvatarInfoList { get; set; } = [];
    public int[] ShowNameCardIdList { get; set; } = [];
    public ProfilePictureData ProfilePicture { get; set; } = null!;

    public int TheaterActIndex { get; set; }
    public int TheaterModeIndex { get; set; }
    public int TheaterStarIndex { get; set; }

    [JsonPropertyName("fetterCount")] public int FetterCount { get; set; }
}

public class AvatarInfoListNode
{
    public int AvatarId { get; set; }
    public int Level { get; set; }
    public int EnergyType { get; set; }
}

public class ProfilePictureData
{
    public int AvatarId { get; set; }
}