using System.Text.Json;
using EnkaSharp.Entities.Base.Raw;

namespace EnkaSharp.Entities.Genshin.Raw;

public class EnkaInfo
{
    public RestPlayerInfo? PlayerInfo { get; set; }
    public int? Ttl { get; set; }
    public string? Uid { get; set; }
    public Owner? Owner { get; set; }


    internal static async Task<EnkaInfo> GetEnkaInfo(HttpClient client, long uid)
    {
        HttpResponseMessage request = await client.GetAsync($"uid/{uid}?info");
        if (!request.IsSuccessStatusCode)
            EnkaClient.HandleError(request.StatusCode);

        await using Stream responseStream = await request.Content.ReadAsStreamAsync();
        var info = await JsonSerializer.DeserializeAsync<EnkaInfo>(responseStream,
            JsonSettings.CamelCase);
        return info ?? throw new InvalidOperationException();
    }
}

public class Owner
{
    public string? Hash { get; set; }
    public string? Username { get; set; }
    public Profile? Profile { get; set; }
    public int Id { get; set; }
}