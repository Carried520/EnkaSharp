namespace EnkaSharp.Entities.Genshin.Abstractions;

/// <summary>
/// Represents Constellation data.
/// </summary>
public class Constellation
{
    public string? Name { get; internal set; }
    public Uri? IconUri { get; internal set; }
    public int Rank { get; internal set; }
}