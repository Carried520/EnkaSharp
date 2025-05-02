namespace EnkaSharp.AssetHandlers;

internal interface IAssetHandler
{
    internal Task DownloadDataAsync();
    internal GameType GameType { get; set; }
}