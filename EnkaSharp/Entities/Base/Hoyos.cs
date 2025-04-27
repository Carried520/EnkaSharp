using System.Text.Json;
using System.Text.Json.Serialization;

namespace EnkaSharp.Entities.Base;

/// <summary>
/// Represents data returned from hoyos endpoint.
/// </summary>
public class Hoyos
{
    public required int Uid { get; set; }
    [JsonPropertyName("uid_public")] public bool IsUidPublic { get; set; }
    [JsonPropertyName("public")] public bool IsPublic { get; set; }
    [JsonPropertyName("live_public")] public bool IsLivePublic { get; set; }
    [JsonPropertyName("verified")] public bool IsVerified { get; set; }
    public required PlayerInfo PlayerInfo { get; set; }
    public required string Hash { get; set; }
    public required string Region { get; set; }
    public int Order { get; set; }
    public Dictionary<string, int> AvatarOrder { get; set; } = [];
    public int HoyoType { get; set; }


    internal static async Task<Hoyos> RequestUserAsync(HttpClient client, JsonSerializerOptions jsonSerializerOptions,
        string name)
    {
        HttpResponseMessage request = await client.GetAsync($"profile/{name}/hoyos/");
        if (!request.IsSuccessStatusCode)
            EnkaClient.HandleError(request.StatusCode);

        await using Stream responseStream = await request.Content.ReadAsStreamAsync();
        Dictionary<string, JsonElement>? deserialized =
            await JsonSerializer.DeserializeAsync<Dictionary<string, JsonElement>>(responseStream,
                jsonSerializerOptions);

        return deserialized?.First().Value.Deserialize<Hoyos>(jsonSerializerOptions) ??
               throw new InvalidOperationException("Could not parse requested user");
    }
}