using System.Text.Json;
using EnkaSharp.Entities.Base.Raw;

namespace EnkaSharp.Entities.Genshin.Abstractions;

public class EnkaGenshinInfo
{
    internal RestPlayerInfo? PlayerInfo { get; set; }
    public int? Ttl { get; internal set; }
    public string? Uid { get; internal set; }
    public Owner? Owner { get; internal set; }


    internal static async Task<EnkaGenshinInfo> GetEnkaInfo(HttpClient client, long uid , CancellationToken cancellationToken)
    {
        HttpResponseMessage request = await client.GetAsync($"uid/{uid}?info" , cancellationToken);
        if (!request.IsSuccessStatusCode)
            EnkaClient.HandleError(request.StatusCode);

        await using Stream responseStream = await request.Content.ReadAsStreamAsync(cancellationToken);
        var info = await JsonSerializer.DeserializeAsync<EnkaGenshinInfo>(responseStream,
            JsonSettings.CamelCase ,  cancellationToken);
        return info ?? throw new InvalidOperationException();
    }
}

public class Owner
{
    public string? Hash { get; internal set; }
    public string? Username { get; internal set; }
    public Profile? Profile { get; internal set; }
    public int Id { get; internal set; }
}