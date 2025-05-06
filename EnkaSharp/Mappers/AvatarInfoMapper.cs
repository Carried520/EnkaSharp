using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Base.Abstractions;
using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Entities.Genshin;
using EnkaSharp.Entities.Genshin.Abstractions;
using EnkaSharp.Utils;

namespace EnkaSharp.Mappers;

internal static class AvatarInfoMapper
{
    internal static Character MapAvatarInfo(RestAvatarInfo restAvatarInfo)
    {
        IAssetHandler handler = EnkaClient.Assets[GameType.Genshin];
        if (handler is not GenshinAssetHandler genshinAssetHandler)
            throw new InvalidCastException("Wrongly registered genshin handler");

        Dictionary<PropMapNodeType, int> propMap = PropMapMapper.MapPropMap(restAvatarInfo.PropMap);
        Dictionary<FightPropType, double> battleMap = PropMapMapper.MapFightProps(restAvatarInfo.FightPropMap);
        CharacterData? textHash = genshinAssetHandler.Data.Characters?[restAvatarInfo.AvatarId.ToString()];

        string? name =
            genshinAssetHandler.Data.Localization?[EnkaClient.Config.Language][
                textHash?.NameTextMapHash.ToString() ?? throw new InvalidOperationException()];
        return new Character(name ?? throw new InvalidOperationException(), propMap, battleMap)
        {
            AvatarId = restAvatarInfo.AvatarId,
            CharacterStats = MapPropMap(restAvatarInfo.PropMap),
            ConstellationIds = restAvatarInfo.TalentIdList,
            Talents = MapTalents(restAvatarInfo.AvatarId,
                restAvatarInfo.SkillDepotId,
                restAvatarInfo.SkillLevelMap,
                restAvatarInfo.ProudSkillExtraLevelMap),
            Weapon = MapWeapon(restAvatarInfo.EquipList),
            Artifacts = MapArtifacts(restAvatarInfo.EquipList)
        };
    }


    private static CharacterStats MapPropMap(Dictionary<string, PropMapNode> propMap)
    {
        Dictionary<PropMapNodeType, int> propMapDto = PropMapMapper.MapPropMap(propMap);
        var stats = new CharacterStats
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
            string? talentName = genshinAssetHandler.GetDataFromTextMap(talentInfo.NameTextMapHash.ToString());
            Uri talentUrl = UriConstants.GetAssetUri(talentInfo.Icon);

            int extraLevel = proudSkillExtraLevelMap.GetValueOrDefault(key, 0);
            talents.Add(new Talent
            {
                Id = skillId,
                Name = talentName,
                Level = baseLevel + extraLevel,
                BaseLevel = baseLevel,
                ExtraLevel = extraLevel,
                IconUri = talentUrl
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