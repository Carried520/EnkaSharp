using EnkaSharp.Entities.Base.Abstractions;

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

    public string Name { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public int AscensionLevel { get; set; }

    public Dictionary<FightPropType, double> BattleStats { get; set; }
}