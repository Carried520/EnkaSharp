using EnkaSharp.Entities.Base.Abstractions;

namespace EnkaSharp.Entities.Genshin;

public class AvatarInfo
{
    public long AvatarId { get; set; }
    public CharacterStats? AvatarStats { get; set; }
    public Dictionary<FightPropType, double> FightPropMap { get; set; } = [];
    public int[] ConstellationIds { get; set; } = [];
    public Talent[] Talents { get; set; } = [];

    public Weapon? Weapon { get; set; }
    public Artifact[] Artifacts { get; set; } = [];
}

public class CharacterStats
{
    public int Experience { get; set; }
    public int AscensionLevel { get; set; }
    public int Satiation { get; set; }
    public int SatationPenalty { get; set; }
    public int Level { get; set; }
    public int Stamina { get; set; }
    public int DiveStamina { get; set; }
}

public class Talent
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Level { get; set; }
    public int BaseLevel { get; set; }
    public int ExtraLevel { get; set; }
    public Uri? IconUri { get; set; }
}