using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Mappers;

namespace EnkaSharp.Entities.Genshin.Abstractions;

public class PlayerInfo
{

    public string? Uid { get; internal set; }
    public string? Nickname { get; internal set; }
    public int Level { get; internal set; }
    public string? Signature { get; internal set; }
    public Uri? NameCardIconUri { get; internal set; }
    public int WorldLevel { get; internal set; }

    public int AchievementCount { get; internal set; }

    public AbyssInfo? Abyss { get; internal set; }
    
    public Uri?[] ShowNameCardUris { get; internal set; } = [];
    public ProfilePictureData? ProfilePicture { get; internal set; }
    public int FetterCount { get; internal set; }
}