using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Base.Abstractions;
using EnkaSharp.Entities.Base.Raw;

namespace EnkaSharp.Mappers;

public static class AvatarInfoMapper
{
    public static AvatarInfo MapAvatarInfo(RestAvatarInfo restAvatarInfo)
    {
        return new AvatarInfo
        {
            AvatarId = restAvatarInfo.AvatarId,
            AvatarStats = MapPropMap(restAvatarInfo.PropMap),
            FightPropMap = MapFightPropMap(restAvatarInfo.FightPropMap),
            ConstellationIds = restAvatarInfo.TalentIdList,
            Talents = MapTalents(restAvatarInfo.AvatarId,
                restAvatarInfo.SkillDepotId,
                restAvatarInfo.SkillLevelMap,
                restAvatarInfo.ProudSkillExtraLevelMap),
            Weapon = MapWeapon(restAvatarInfo.EquipList),
            Artifacts = MapArtifacts(restAvatarInfo.EquipList)
        };
    }


    private static AvatarStats MapPropMap(Dictionary<string, PropMapNode> propMap)
    {
        Dictionary<PropMapNodeType, int> propMapDto = PropMapMapper.MapPropMap(propMap);
        var stats = new AvatarStats
        {
            Experience = propMapDto[PropMapNodeType.Experience],
            AscensionLevel = propMapDto[PropMapNodeType.AscensionLevel],
            Satiation = propMapDto[PropMapNodeType.Satiation],
            SatationPenalty = propMapDto[PropMapNodeType.SatationPenalty],
            Level = propMapDto[PropMapNodeType.Level],
            Stamina = propMapDto[PropMapNodeType.Stamina] / 100,
            DiveStamina = propMapDto[PropMapNodeType.DiveStamina] / 100
        };
        return stats;
    }

    private static Dictionary<FightPropType, double> MapFightPropMap(Dictionary<string, double> fightMapNodes)
    {
        return PropMapMapper.MapFightProps(fightMapNodes);
    }


    private static Talent[] MapTalents(long characterId, int skillDepotId, Dictionary<string, int> skillLevelMap,
        Dictionary<string, int> proudSkillExtraLevelMap)
    {
        var talents = new List<Talent>();
        if (skillLevelMap.Count == 0)
            return talents.ToArray();

        IAssetHandler handler = EnkaClient.Assets[GameType.Genshin];
        if (handler is not GenshinAssetHandler genshinAssetHandler)
            throw new InvalidCastException("Wrong handler initialized for Genshin");


        foreach ((string? key, int baseLevel) in skillLevelMap)
        {
            if (!int.TryParse(key, out int skillId))
                continue;

            TalentData talentInfo = genshinAssetHandler.Data.Talents?[skillId.ToString()] ?? throw new
                InvalidOperationException();
            string talentName = genshinAssetHandler.Data.TextMap?["en"][talentInfo.NameTextMapHash.ToString()] ??
                                throw new InvalidOperationException();
            var talentUrl = $"https://enka.network/ui/{talentInfo.Icon}.png";

            int extraLevel = proudSkillExtraLevelMap.GetValueOrDefault(key, 0);
            talents.Add(new Talent
            {
                Id = skillId,
                Name = talentName,
                Level = baseLevel + extraLevel,
                BaseLevel = baseLevel,
                ExtraLevel = extraLevel,
                IconUri = new Uri(talentUrl)
            });
        }

        return talents.ToArray();
    }


    private static Artifact[] MapArtifacts(EquipItem[] items)
    {
        return items.Where(item => item.GetEquipmentType() is EquipmentType.Artifact)
            .Select(EquipmentMapper.MapArtifact).ToArray();
    }

    private static Weapon? MapWeapon(EquipItem[] items)
    {
        EquipItem? foundWeapon =
            items.FirstOrDefault(equipItem => equipItem.GetEquipmentType() is EquipmentType.Weapon);
        return foundWeapon is null ? null : EquipmentMapper.MapWeapon(foundWeapon);
    }
}

public class AvatarInfo
{
    public long AvatarId { get; set; }
    public AvatarStats? AvatarStats { get; set; }
    public Dictionary<FightPropType, double> FightPropMap { get; set; } = [];
    public int[] ConstellationIds { get; set; } = [];
    public Talent[] Talents { get; set; } = [];

    public Weapon? Weapon { get; set; }
    public Artifact[] Artifacts { get; set; } = [];
}

public class AvatarStats
{
    public int Experience { get; set; }
    public int AscensionLevel { get; set; }
    public int Satiation { get; set; }
    public int SatationPenalty { get; set; }
    public int Level { get; set; }
    public int Stamina { get; set; }
    public int DiveStamina { get; set; }
}

public class Talent
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Level { get; set; }
    public int BaseLevel { get; set; }
    public int ExtraLevel { get; set; }
    public Uri? IconUri { get; set; }
}