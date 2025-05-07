using System.Net.Http.Json;

namespace EnkaSharp.AssetHandlers.Genshin;

internal class GenshinAssetHandler : IAssetHandler
{
    public GenshinAssetData Data { get; set; } = new();

    private static readonly HttpClient HttpClient = new()
        { BaseAddress = new Uri("https://raw.githubusercontent.com/EnkaNetwork/API-docs/refs/heads/master/store/") };


    public async Task DownloadDataAsync()
    {
        await DownloadGenshinAssetsAsync();
    }

    public GameType GameType { get; set; } = GameType.Genshin;

    internal string? GetDataFromTextMap(string nameTextMapHash)
    {
        Dictionary<string, string>? localizedTextMap = Data.TextMap?[EnkaClient.Config.Language];
        return localizedTextMap?[nameTextMapHash];
    }

    private async Task DownloadGenshinAssetsAsync()
    {
        Data.Localization =
            await HttpClient.GetFromJsonAsync<Dictionary<string, Dictionary<string, string?>>>(
                "loc.json");
        Data.Characters = await HttpClient.GetFromJsonAsync<Dictionary<string, CharacterData>>(
            "characters.json");
        Data.NameCards = await HttpClient.GetFromJsonAsync<Dictionary<string, NameCard>>("namecards.json");
        Data.ProfilePictures = await HttpClient.GetFromJsonAsync<Dictionary<string, ProfilePicture>>("pfps.json");
        Data.TextMap =
            await HttpClient.GetFromJsonAsync<Dictionary<string, Dictionary<string, string>>>(
                "https://raw.githubusercontent.com/seriaati/enka-py-assets/main/data/text_map.json");
        Data.Talents = await HttpClient.GetFromJsonAsync<Dictionary<string, TalentData>>(
            "https://raw.githubusercontent.com/seriaati/enka-py-assets/main/data/talents.json");
        Data.Constellations =
            await HttpClient.GetFromJsonAsync<Dictionary<string, ConstellationData>>(
                "https://raw.githubusercontent.com/seriaati/enka-py-assets/main/data/consts.json");
        
    }
}