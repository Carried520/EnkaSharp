namespace EnkaSharp.Utils;

public class UriConstants
{
    private const string GenshinBaseAssetUri = "https://enka.network/ui/";

    public static Uri GetAssetUri(string? resourceName) => new($"{GenshinBaseAssetUri}{resourceName}.png");
}