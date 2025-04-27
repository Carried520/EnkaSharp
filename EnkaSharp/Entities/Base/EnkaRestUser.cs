using System.Text.Json;

namespace EnkaSharp.Entities.Base;

public record EnkaRestUser
{
    public required PlayerInfo PlayerInfo { get; init; }
    public AvatarInfo[] AvatarInfoList { get; init; } = [];
    public int Ttl { get; init; }
    public string? Uid { get; init; }
    public Owner? Owner { get; init; }

    internal static async Task<EnkaRestUser> GetUserAsync(HttpClient client, long uid)
    {
        HttpResponseMessage request = await client.GetAsync($"uid/{uid}");
        if (!request.IsSuccessStatusCode)
            EnkaClient.HandleError(request.StatusCode);

        await using Stream responseStream = await request.Content.ReadAsStreamAsync();
        EnkaRestUser? user = await JsonSerializer.DeserializeAsync<EnkaRestUser>(responseStream,
            JsonSettings.CamelCase);
        return user ?? throw new InvalidOperationException();
    }

    internal async Task<EnkaUser> ToUserAsync()
    {
        return new EnkaUser();
    }
}