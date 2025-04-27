using System.Text.Json.Serialization;

namespace EnkaSharp.Entities.Base;

public record AvatarInfo
{
    public int AvatarId { get; init; }

    public Dictionary<string, PropMapNode> PropMap { get; init; } = [];

    public int[] TalentIdList { get; init; } = [];

    public Dictionary<string, double> FightPropMap { get; init; } = [];
    
    public int SkillDepotId { get; init; }
    public Dictionary<string, int> SkillLevelMap { get; init; } = [];
    public Dictionary<string, int> ProudSkillExtraLevelMap { get; init; } = [];
    public EquipItem[] EquipList { get; init; } = [];
    public FetterInfo? FetterInfo { get; init; }
}

public record PropMapNode
{
    public int Type { get; init; }

    [JsonPropertyName("val")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Val { get; init; }
}

public record EquipItem
{
    public int ItemId { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Reliquary? Reliquary { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Weapon? Weapon { get; init; }

    public FlatStats? Flat { get; init; }
}

public record FetterInfo(int ExpLevel);

public record Reliquary(int Level, int MainPropId, int[] AppendPropIdList);

public record Weapon(int Level, int PromoteLevel, Dictionary<string, int> AffixMap);

public record FlatStats(string NameTextMapHash, int RankLevel, string ItemType, string Icon)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ItemType { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? SetId { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SetNameTextMapHash { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BaseEquipStats[]? ReliquarySubstats { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public BaseEquipStats[]? WeaponStats { get; init; }
}

public record BaseEquipStats
{
    public string? AppendPropId { get; init; }
    public double? StatValue { get; init; }
}