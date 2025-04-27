using System.Text.Json;

namespace EnkaSharp.Entities.Base;

public record EnkaInfo
{
    public required PlayerInfo PlayerInfo { get; init; }
    public int? Ttl { get; init; }
    public string? Uid { get; init; }
    public Owner? Owner { get; init; }


    internal static async Task<EnkaInfo> GetEnkaInfo(HttpClient client, long uid)
    {
        HttpResponseMessage request = await client.GetAsync($"uid/{uid}?info");
        if (!request.IsSuccessStatusCode)
            EnkaClient.HandleError(request.StatusCode);

        await using Stream responseStream = await request.Content.ReadAsStreamAsync();
        EnkaInfo? info = await JsonSerializer.DeserializeAsync<EnkaInfo>(responseStream,
            JsonSettings.CamelCase);
        return info ?? throw new InvalidOperationException();
    }
}

public record Owner
{
    public string? Hash { get; init; }
    public string? Username { get; init; }
    public Profile? Profile { get; init; }
    public int Id { get; init; }
}