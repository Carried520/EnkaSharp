namespace EnkaSharp.Entities.Genshin.Abstractions;

public class Character
{
    public Character(string name, Dictionary<PropMapNodeType, int> propMapNodes,
        Dictionary<FightPropType, double> battleStats)
    {
        Name = name;
        Level = propMapNodes[PropMapNodeType.Level];
        Experience = propMapNodes[PropMapNodeType.Experience];
        AscensionLevel = propMapNodes[PropMapNodeType.AscensionLevel];
        BattleStats = battleStats;
    }

    public string Name { get; internal set; }
    public int Level { get; internal set; }
    public int Experience { get; internal set; }
    public int AscensionLevel { get; internal set; }

    public Dictionary<FightPropType, double> BattleStats { get; internal set; }


    public long AvatarId { get; internal set; }
    public CharacterStats? CharacterStats { get; internal set; }
    public Talent[] Talents { get; internal set; } = [];

    public Weapon? Weapon { get; internal set; }
    public Artifact[] Artifacts { get; internal set; } = [];
    public Constellation[] Constellations { get; internal set; } = [];
}