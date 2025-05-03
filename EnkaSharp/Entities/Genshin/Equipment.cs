using EnkaSharp.Entities.Base.Abstractions;

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
}