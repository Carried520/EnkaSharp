using EnkaSharp.Entities.Base.Abstractions;
using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Entities.Genshin.Raw;
using EnkaSharp.Mappers;

namespace EnkaSharp.Entities.Genshin.Abstractions;

public class EnkaGenshinData : IGenshinData
{
    public EnkaGenshinData(RestPlayerInfo? playerInfo, RestAvatarInfo[] restAvatarInfos, int ttl, string? uid,
        Owner? owner)
    {
        Player = PlayerInfoMapper.MapPlayerInfo(playerInfo ?? throw new ArgumentNullException(nameof(playerInfo)), uid);
        AvatarInfoList = restAvatarInfos.Select(AvatarInfoMapper.MapAvatarInfo).ToArray();
        Ttl = ttl;
        Owner = owner;
    }

    public AvatarInfo[] AvatarInfoList { get; set; }
    public PlayerInfo Player { get; set; }
    internal int Ttl { get; set; }
    public Owner? Owner { get; set; }
    public Character[] Characters { get; set; } = [];
    public int Stamina { get; set; }
}