using System.Text.Json.Serialization;

namespace EnkaSharp.Entities.Base.Raw;

public class Profile
{
    public string? Bio { get; set; }
    public int Level { get; set; }
    public Uri? Avatar { get; set; }
    [JsonPropertyName("image_url")] public Uri? ImageUrl { get; set; }
}