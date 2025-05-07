using EnkaSharp.AssetHandlers;
using EnkaSharp.AssetHandlers.Genshin;
using EnkaSharp.Entities.Base.Raw;
using EnkaSharp.Entities.Genshin;
using EnkaSharp.Entities.Genshin.Abstractions;
using EnkaSharp.Entities.Genshin.Raw;
using EnkaSharp.Utils;

namespace EnkaSharp.Mappers;

internal static class EquipmentMapper
{
    internal static Weapon MapWeapon(EquipItem equipItem)
    {
        if (equipItem.GetEquipmentType() is not EquipmentType.Weapon)
            throw new InvalidOperationException();
        if (equipItem.Weapon == null || equipItem.Flat?.WeaponStats == null || equipItem.Flat.ItemType == null)
            throw new InvalidOperationException();

        var genshinAssetHandler = EnkaClient.GetAssets<GenshinAssetHandler>(GameType.Genshin);
        
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
            IconUri = UriConstants.GetAssetUri(equipItem.Flat.Icon)
        };
    }

    internal static Artifact MapArtifact(EquipItem equipItem)
    {
        var genshinAssetHandler = EnkaClient.GetAssets<GenshinAssetHandler>(GameType.Genshin);
        if (equipItem.GetEquipmentType() is not EquipmentType.Artifact)
            throw new InvalidOperationException();
        if (equipItem.Reliquary == null || equipItem.Flat?.ReliquarySubstats == null ||
            equipItem.Flat.EquipType == null)
            throw new InvalidOperationException();


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
            Uri = UriConstants.GetAssetUri(equipItem.Flat.Icon),
            SlotType = GetArtifactSlotType(equipItem.Flat.EquipType)
        };
    }
    

    private static ArtifactSlotType GetArtifactSlotType(string value) => value switch
    {
        "EQUIP_BRACER" => ArtifactSlotType.Flower,
        "EQUIP_NECKLACE" => ArtifactSlotType.Plume,
        "EQUIP_SHOES" => ArtifactSlotType.Sands,
        "EQUIP_RING" => ArtifactSlotType.Goblet,
        "EQUIP_DRESS" => ArtifactSlotType.Circlet,
        _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
    };
}