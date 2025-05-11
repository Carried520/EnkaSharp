using System.ComponentModel;
using EnkaSharp.Entities.Genshin.Abstractions;

namespace EnkaSharp.Entities.Genshin;

/// <summary>
/// Represents Stat for Artifacts and Weapons
/// </summary>
public class Stat
{
    public FightPropType StatType { get; internal set; }
    public double Value { get; internal set; }
}

/// <summary>
/// Represents an abstraction over weapon
/// </summary>
public class Weapon
{
    public string Name { get; internal set; } = null!;
    public int ItemId { get; internal set; }

    public int Level { get; internal set; }
    public int Ascension { get; internal set; }
    public int Refinement { get; internal set; }
    public int Rarity { get; internal set; }
    public double BaseAttack { get; internal set; }


    public Stat SecondaryStat { get; internal set; } = null!;

    public Uri? IconUri { get; internal set; }
    
}

/// <summary>
/// Represents an abstraction over Artifact
/// </summary>
public class Artifact
{
    public int ItemId { get; internal set; }
    public int Level { get; internal set; }
    public int Rarity { get; internal set; }

    public Stat MainStat { get; internal set; } = null!;
    public Stat[] SubStats { get; internal set; } = null!;
    public string? SetName { get; internal set; } = null!;
    public string? Name { get; internal set; } = null!;
    public Uri? Uri { get; internal set; }

    public ArtifactSlotType SlotType { get; internal set; }
}

/// <summary>
/// Represents type of Artifact
/// </summary>
public enum ArtifactSlotType
{
    Unknown = 0,
    [Description("EQUIP_BRACER")] Flower,
    [Description("EQUIP_NECKLACE")] Plume,
    [Description("EQUIP_SHOES")] Sands,
    [Description("EQUIP_RING")] Goblet,
    [Description("EQUIP_DRESS")] Circlet
}