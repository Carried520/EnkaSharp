using System.Text.Json;
using System.Text.Json.Serialization;

namespace Enka.Client.Entities;

/// <summary>
/// Represents data returned from Snapshot endpoint.
/// </summary>
public record Snapshot
{
    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public required string Username { get; init; }
    public required int Id { get; init; }
    public required Profile Profile { get; init; }


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