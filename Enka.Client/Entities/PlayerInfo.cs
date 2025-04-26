using System.Text.Json.Serialization;

namespace Enka.Client.Entities;

/// <summary>
/// Represents PlayerInfo.
/// </summary>
public class PlayerInfo
{
    public int Level { get; set; }
    public required string Nickname { get; set; }
    public required string Signature { get; set; }
    [JsonPropertyName("nameCardId")] public required int NameCardId { get; set; }
    [JsonPropertyName("worldLevel")] public required int WorldLevel { get; set; }
    [JsonPropertyName("fetterCount")] public required int FetterCount { get; set; }
    
    
}