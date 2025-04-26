using System.Text.Json.Serialization;

namespace Enka.Client.Entities;

public record Profile
{
    public required string Bio { get; init; }
    public required int Level { get; init; }
    public required Uri Avatar { get; init; }
    [JsonPropertyName("image_url")] public required Uri ImageUrl { get; init; }
}