using System.ComponentModel;

namespace EnkaSharp.AssetHandlers;

public enum GameType
{
    [Description("Genshin Impact")] Genshin,
    [Description("Honkai Star Rail")] Hsr,
    [Description("Zenless Zone Zero")] Zzz
}