using System.Text.Json;
using EnkaSharp.Entities.Base.Raw;

namespace EnkaSharp.Entities.Genshin.Raw;

public class EnkaGenshinInfo
{
    public RestPlayerInfo? PlayerInfo { get; set; }
    public int? Ttl { get; set; }
    public string? Uid { get; set; }
    public Owner? Owner { get; set; }


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
    public string? Hash { get; set; }
    public string? Username { get; set; }
    public Profile? Profile { get; set; }
    public int Id { get; set; }
}