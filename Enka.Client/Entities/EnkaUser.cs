using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;

namespace Enka.Client.Entities;

public class EnkaUser
{
    
    
    public required int Uid { get; set; }
    [JsonPropertyName("uid_public")] public bool IsUidPublic { get; set; }
    [JsonPropertyName("public")] public bool IsPublic { get; set; }
    [JsonPropertyName("live_public")] public bool IsLivePublic { get; set; }
    [JsonPropertyName("verified")] public bool IsVerified { get; set; }
    public required UserInfo PlayerInfo { get; set; }
    public required string Hash { get; set; }
    public required string Region { get; set; }
    public int Order { get; set; }
    public Dictionary<string, int> AvatarOrder { get; set; } = [];
    public int HoyoType { get; set; }
    
}