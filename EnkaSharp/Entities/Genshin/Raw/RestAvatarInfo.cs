using System.Text.Json.Serialization;

namespace EnkaSharp.Entities.Base.Raw;

public class RestAvatarInfo
{
    public int AvatarId { get; set; }

    public Dictionary<string, PropMapNode> PropMap { get; set; } = [];

    public int[] TalentIdList { get; set; } = [];

    public Dictionary<string, double> FightPropMap { get; set; } = [];

    public int SkillDepotId { get; set; }
    public Dictionary<string, int> SkillLevelMap { get; set; } = [];
    public Dictionary<string, int> ProudSkillExtraLevelMap { get; set; } = [];
    public EquipItem[] EquipList { get; set; } = [];
    public FetterInfo? FetterInfo { get; set; }
}

public class PropMapNode
{
    public int Type { get; set; }

    [JsonPropertyName("val")] public string? Val { get; set; }
}

public class EquipItem
{
    public int ItemId { get; set; }

    public Reliquary? Reliquary { get; set; }

    public RestWeapon? Weapon { get; set; }

    public FlatStats? Flat { get; set; }

    public EquipmentType GetEquipmentType()
    {
        if (Weapon is not null)
            return EquipmentType.Weapon;
        if (Reliquary is not null)
            return EquipmentType.Artifact;
        throw new InvalidOperationException("Unknown equipment type");
    }
}

public class FetterInfo
{
    public int ExpLevel { get; set; }
}

public class Reliquary
{
    public int Level { get; set; }
    public int MainPropId { get; set; }
    public int[] AppendPropIdList { get; set; } = [];
}

public class RestWeapon
{
    public int Level { get; set; }
    public int PromoteLevel { get; set; }
    public Dictionary<string, int> AffixMap { get; set; } = [];
}

public enum EquipmentType
{
    Weapon,
    Artifact
}

public class FlatStats
{
    public string? ItemType { get; init; }
    
    public int? SetId { get; init; }
    
    public string? SetNameTextMapHash { get; init; }
    
    public BaseEquipStats[]? ReliquarySubstats { get; init; }
    
    public BaseEquipStats[]? WeaponStats { get; init; }

    public string? NameTextMapHash { get; set; }
    public int RankLevel { get; set; }
    public string? Icon { get; set; }
    
    public MainProp? ReliquaryMainstat { get; set; }
}

public class BaseEquipStats
{
    public string? AppendPropId { get; set; }
    public double StatValue { get; set; }
}

public class MainProp
{
    public string? MainPropId { get; set; }
    public double StatValue { get; set; }
}