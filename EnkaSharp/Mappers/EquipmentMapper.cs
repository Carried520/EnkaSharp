using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Base.Abstractions;
using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Entities.Genshin;
using EnkaSharp.Utils;

namespace EnkaSharp.Mappers;

internal static class EquipmentMapper
{
    internal static Weapon MapWeapon(EquipItem equipItem)
    {
        if (equipItem.GetEquipmentType() is not EquipmentType.Weapon)
            throw new InvalidOperationException();
        if (equipItem.Weapon == null || equipItem.Flat?.WeaponStats == null)
            throw new InvalidOperationException();

        IAssetHandler handler = EnkaClient.Assets[GameType.Genshin];
        if (handler is not GenshinAssetHandler genshinAssetHandler)
            throw new InvalidCastException();


        BaseEquipStats? baseAttack =
            equipItem.Flat.WeaponStats.FirstOrDefault(flatStat =>
                flatStat.AppendPropId == "FIGHT_PROP_BASE_ATTACK");
        BaseEquipStats? secondaryStatNode =
            equipItem.Flat.WeaponStats.FirstOrDefault(flatStat =>
                flatStat.AppendPropId is not "FIGHT_PROP_BASE_ATTACK");
        var secondaryStat = new Stat
        {
            StatType = FightProps.FightPropMap[secondaryStatNode?.AppendPropId ?? throw new
                InvalidOperationException()],
            Value = secondaryStatNode.StatValue
        };

        string? name = genshinAssetHandler.GetDataFromTextMap(equipItem.Flat.NameTextMapHash ??
                                                              throw new InvalidOperationException(
                                                                  "Error parsing data"));

        return new Weapon
        {
            ItemId = equipItem.ItemId,
            Level = equipItem.Weapon.Level,
            Ascension = equipItem.Weapon.PromoteLevel,
            Refinement = equipItem.Weapon.AffixMap.FirstOrDefault().Value,
            Rarity = equipItem.Flat.RankLevel,
            BaseAttack = baseAttack?.StatValue ?? 0,
            SecondaryStat = secondaryStat,
            Name = name ?? $"Genshin_Weapon_{equipItem.ItemId}",
            IconUri = UriConstants.GetAssetUri(equipItem.Flat.Icon),
        };
    }

    internal static Artifact MapArtifact(EquipItem equipItem)
    {
        IAssetHandler handler = EnkaClient.Assets[GameType.Genshin];
        if (equipItem.GetEquipmentType() is not EquipmentType.Artifact)
            throw new InvalidOperationException();
        if (equipItem.Reliquary == null || equipItem.Flat?.ReliquarySubstats == null)
            throw new InvalidOperationException();
        if (handler is not GenshinAssetHandler genshinAssetHandler)
            throw new InvalidCastException();

        string? setName = genshinAssetHandler.GetDataFromTextMap(equipItem.Flat.SetNameTextMapHash ??
                                                                 throw new InvalidOperationException(
                                                                     "Error parsing data"));

        string? name = genshinAssetHandler.GetDataFromTextMap(equipItem.Flat.NameTextMapHash ?? throw new
            InvalidOperationException("Error parsing data"));


        Stat[] substats = equipItem.Flat.ReliquarySubstats.Select(subStat => new Stat
        {
            StatType = FightProps.FightPropMap[
                subStat.AppendPropId?.ToString() ?? throw new InvalidOperationException()],
            Value = subStat.StatValue
        }).ToArray();

        return new Artifact
        {
            ItemId = equipItem.ItemId,
            Level = equipItem.Reliquary.Level > 0 ? equipItem.Reliquary.Level - 1 : equipItem.Reliquary.Level,
            Rarity = equipItem.Flat.RankLevel,
            MainStat = new Stat
            {
                StatType = FightProps.FightPropMap[
                    equipItem.Flat.ReliquaryMainstat?.MainPropId ?? throw new InvalidOperationException()],
                Value = equipItem.Flat.ReliquaryMainstat.StatValue
            },
            SubStats = substats,
            SetName = setName,
            Name = name,
            Uri = UriConstants.GetAssetUri(equipItem.Flat.Icon)
        };
    }
}