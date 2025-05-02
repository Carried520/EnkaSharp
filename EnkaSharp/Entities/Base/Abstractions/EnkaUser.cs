using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Mappers;

namespace EnkaSharp.Entities.Base.Abstractions;

public class EnkaUser : IEnkaUser
{
    public EnkaUser(RestPlayerInfo? playerInfo, RestAvatarInfo[] restAvatarInfos , int ttl, string? uid, Owner? owner)
    {
        PlayerInfo = PlayerInfoMapper.MapPlayerInfo(playerInfo ?? throw new ArgumentNullException(nameof(playerInfo)));
        AvatarInfoList = restAvatarInfos.Select(AvatarInfoMapper.MapAvatarInfo).ToArray();
        Ttl = ttl;
        Uid = uid;
        Owner = owner;
    }

    public AvatarInfo[] AvatarInfoList { get; set; } = [];
    public PlayerInfo PlayerInfo { get; set; }
    internal int Ttl { get; set; }
    public string? Uid { get; set; }
    public Owner? Owner { get; set; }
    public Character[] Characters { get; set; } = [];
    public int Stamina { get; set; }
}