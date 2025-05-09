namespace EnkaSharp.Entities.Genshin.Abstractions;

/// <summary>
/// Represents Single Character in Genshin.
/// </summary>
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

    /// <summary>
    /// Character's Name
    /// </summary>
    public string Name { get; internal set; }
    /// <summary>
    /// Character's Level
    /// </summary>
    public int Level { get; internal set; }
    /// <summary>
    /// Character's Experience
    /// </summary>
    public int Experience { get; internal set; }
    /// <summary>
    /// Character's Ascension Level
    /// </summary>
    public int AscensionLevel { get; internal set; }
    /// <summary>
    /// All damage stats such as Crit Rate , Crit Dmg , Attack that Character has
    /// </summary>

    public Dictionary<FightPropType, double> BattleStats { get; internal set; }


    /// <summary>
    /// Character Id
    /// </summary>
    public long AvatarId { get; internal set; }
    
    /// <summary>
    /// Basic non-battle related stats
    /// </summary>
    public CharacterStats? CharacterStats { get; internal set; }
    
    /// <summary>
    /// Info about Talents
    /// </summary>
    public Talent[] Talents { get; internal set; } = [];

    /// <summary>
    /// Gets weapon if it exists
    /// </summary>
    public Weapon? Weapon { get; internal set; }
    /// <summary>
    /// Gets Artifacts
    /// </summary>
    public Artifact[] Artifacts { get; internal set; } = [];
    
    /// <summary>
    /// Gets unlocked Constellations
    /// </summary>
    public Constellation[] Constellations { get; internal set; } = [];
}