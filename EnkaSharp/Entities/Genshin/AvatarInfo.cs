namespace EnkaSharp.Entities.Genshin;

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