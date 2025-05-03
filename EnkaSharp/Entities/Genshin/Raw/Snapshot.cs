using System.Text.Json;

namespace EnkaSharp.Entities.Base.Raw;

/// <summary>
/// Represents data returned from Snapshot endpoint.
/// </summary>
public class Snapshot
{
    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public string? Username { get; set; }
    public int Id { get; set; }
    public Profile? Profile { get; set; }


    internal static async Task<Snapshot> RequestSnapshotAsync(HttpClient client, string name)
    {
        HttpResponseMessage request = await client.GetAsync($"profile/{name}");
        if (!request.IsSuccessStatusCode)
            EnkaClient.HandleError(request.StatusCode);
        await using Stream responseStream = await request.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<Snapshot>(responseStream,
                   JsonSerializerOptions) ??
               throw new InvalidOperationException("Error while getting snapshot");
    }
}