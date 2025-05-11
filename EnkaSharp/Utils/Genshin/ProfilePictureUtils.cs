using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;

namespace EnkaSharp.Utils.Genshin;

public static class ProfilePictureUtils
{
    public static bool TryGetCharacterIconUri(int characterId, out Uri? uri)
    {
        uri = null;
        var handler = EnkaClient.GetAssets<GenshinAssetHandler>(GameType.Genshin);
        
        if (handler.Data.Characters == null ||
            !handler.Data.Characters.TryGetValue(characterId.ToString(), out CharacterData? characterData))
            return false;
        
        if (string.IsNullOrEmpty(characterData?.SideIconName))
            return false;
        
        string replacer = characterData.SideIconName.Replace("UI_AvatarIcon_Side_", "UI_AvatarIcon_");
        uri = UriConstants.GetAssetUri(replacer , GameType.Genshin);
        return true;
    }
}