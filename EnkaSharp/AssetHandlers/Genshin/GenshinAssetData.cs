using System.Text.Json.Serialization;

namespace EnkaSharp.AssetHandlers.Genshin;

public class GenshinAssetData
{
    public Dictionary<string, Dictionary<string, string?>>? Localization { get; set; } = [];
    public Dictionary<string, CharacterData>? Characters { get; set; } = [];
    public Dictionary<string, NameCard>? NameCards { get; set; } = [];
    public Dictionary<string, ProfilePicture>? ProfilePictures { get; set; } = [];
    public Dictionary<string, TalentData>? Talents { get; set; } = [];

    public Dictionary<string, Dictionary<string, string>>? TextMap { get; set; } = [];

    public Dictionary<string, ConstellationData>? Constellations { get; set; } = [];
}

public class ConstellationData
{
    public long NameTextMapHash { get; set; }

    public string Icon { get; set; } = null!;
}

public class TalentData
{
    public long NameTextMapHash { get; set; }
    public string? Icon { get; set; }
}

public class ProfilePicture
{
    public string? IconPath { get; set; }
}

public class NameCard
{
    public string? Icon { get; set; }
}

public class CharacterData
{
    [JsonInclude] public string? Element { get; set; }

    [JsonInclude]
    [JsonPropertyName("Consts")]
    public string[] Constellations { get; set; } = [];

    [JsonInclude] public int[] SkillOrder { get; set; } = [];

    [JsonInclude] public Dictionary<string, string> Skills { get; set; } = [];

    [JsonInclude] public Dictionary<string, int> ProudMap { get; set; } = [];

    [JsonInclude]
    [JsonPropertyName("NameTextMapHash")]
    public long NameTextMapHash { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(SideIconName))]
    public string? SideIconName { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(QualityType))]
    public string? QualityType { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(WeaponType))]
    public string? WeaponType { get; set; }

    [JsonInclude]
    [JsonPropertyName(nameof(Costumes))]
    public Dictionary<string, CostumeNode> Costumes { get; set; } = [];
}

public class CostumeNode
{
    public string? SideIconName { get; set; }
    public string? Icon { get; set; }
    public string? Art { get; set; }
    public int AvatarId { get; set; }
}