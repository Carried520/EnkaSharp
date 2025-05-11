namespace EnkaSharp.Entities.Genshin;

/// <summary>
/// Wrapper to strongly represent non-battle Character Stats
/// </summary>
public class CharacterStats
{
    public int Experience { get; internal set; }
    public int AscensionLevel { get; internal set; }
    public int Satiation { get; internal set; }
    public int SatationPenalty { get; internal set; }
    public int Level { get; internal set; }
    public int Stamina { get; internal set; }
    public int DiveStamina { get; internal set; }
}

/// <summary>
/// Represents abstraction over Talent.
/// </summary>
public class Talent
{
    public int Id { get; internal set; }
    public string? Name { get; internal set; }
    public int Level { get; internal set; }
    public int BaseLevel { get; internal set; }
    public int ExtraLevel { get; internal set; }
    public Uri? IconUri { get; internal set; }
}