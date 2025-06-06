using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Entities.Genshin.Raw;
using EnkaSharp.Mappers;

namespace EnkaSharp.Entities.Genshin.Abstractions;

/// <summary>
/// Wrapper for data from enka.network API
/// </summary>
public class EnkaGenshinData : IGenshinData
{
    public EnkaGenshinData(RestPlayerInfo? playerInfo, int ttl, string? uid,
        Owner? owner)
    {
        Player = PlayerInfoMapper.MapPlayerInfo(playerInfo ?? throw new ArgumentNullException(nameof(playerInfo)), uid);
        Ttl = ttl;
        Owner = owner;
    }

    public PlayerInfo Player { get; internal set; }
    internal int Ttl { get; set; }
    public Owner? Owner { get; set; }
    public Character[] Characters { get; internal set; } = [];
}