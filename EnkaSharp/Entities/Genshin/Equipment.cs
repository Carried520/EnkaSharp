using System.ComponentModel;
using EnkaSharp.Entities.Genshin.Abstractions;

namespace EnkaSharp.Entities.Genshin;

public class Stat
{
    public FightPropType StatType { get; set; }
    public double Value { get; set; }
}

public class Weapon
{
    public string Name { get; set; } = null!;
    public int ItemId { get; set; }

    public int Level { get; set; }
    public int Ascension { get; set; }
    public int Refinement { get; set; }
    public int Rarity { get; set; }
    public double BaseAttack { get; set; }


    public Stat SecondaryStat { get; set; } = null!;

    public Uri? IconUri { get; set; }

    public WeaponType WeaponType { get; set; }
}

public enum WeaponType
{
    Unknown = 0,
    [Description("WEAPON_SWORD_ONE_HAND")] Sword,
    [Description("WEAPON_CLAYMORE")] Claymore,
    [Description("WEAPON_POLE")] Polearm,
    [Description("WEAPON_BOW")] Bow,
    [Description("WEAPON_CATALYST")] Catalyst
}

public class Artifact
{
    public int ItemId { get; set; }
    public int Level { get; set; }
    public int Rarity { get; set; }

    public Stat MainStat { get; set; } = null!;
    public Stat[] SubStats { get; set; } = null!;
    public string? SetName { get; set; } = null!;
    public string? Name { get; set; } = null!;
    public Uri? Uri { get; set; }

    public ArtifactSlotType SlotType { get; set; }
}

public enum ArtifactSlotType
{
    Unknown = 0,
    [Description("EQUIP_BRACER")] Flower,
    [Description("EQUIP_NECKLACE")] Plume,
    [Description("EQUIP_SHOES")] Sands,
    [Description("EQUIP_RING")] Goblet,
    [Description("EQUIP_DRESS")] Circlet
}