namespace EnkaSharp.Entities.Base.Abstractions;

public static class FightProps
{
    public static readonly Dictionary<string, FightPropType> FightPropMap = new()
    {
        { "FIGHT_PROP_HP", FightPropType.BaseHp },
        { "FIGHT_PROP_ATTACK", FightPropType.BaseAttack },
        { "FIGHT_PROP_DEFENSE", FightPropType.BaseDefense },
        { "FIGHT_PROP_BASE_HP", FightPropType.FlatHp },
        { "FIGHT_PROP_BASE_ATTACK", FightPropType.FlatAttack },
        { "FIGHT_PROP_BASE_DEFENSE", FightPropType.FlatDefense },
        { "FIGHT_PROP_HP_PERCENT", FightPropType.HpPercentage },
        { "FIGHT_PROP_ATTACK_PERCENT", FightPropType.AttackPercentage },
        { "FIGHT_PROP_DEFENSE_PERCENT", FightPropType.DefensePercentage },
        { "FIGHT_PROP_CRITICAL", FightPropType.CritRate },
        { "FIGHT_PROP_CRITICAL_HURT", FightPropType.CritDamage },
        { "FIGHT_PROP_CHARGE_EFFICIENCY", FightPropType.EnergyRecharge },
        { "FIGHT_PROP_ELEMENT_MASTERY", FightPropType.ElementalMastery },
        { "FIGHT_PROP_HEAL_ADD", FightPropType.HealingBonus },
        { "FIGHT_PROP_PHYSICAL_SUB_HURT", FightPropType.PhysicalResistance },
        { "FIGHT_PROP_FIRE_SUB_HURT", FightPropType.PyroResistance },
        { "FIGHT_PROP_ELEC_SUB_HURT", FightPropType.ElectroResistance },
        { "FIGHT_PROP_WATER_SUB_HURT", FightPropType.HydroResistance },
        { "FIGHT_PROP_GRASS_SUB_HURT", FightPropType.DendroResistance },
        { "FIGHT_PROP_WIND_SUB_HURT", FightPropType.AnemoResistance },
        { "FIGHT_PROP_ROCK_SUB_HURT", FightPropType.GeoResistance },
        { "FIGHT_PROP_ICE_SUB_HURT", FightPropType.CryoResistance },
        { "FIGHT_PROP_PHYSICAL_ADD_HURT", FightPropType.PhysicalDamageBonus },
        { "FIGHT_PROP_FIRE_ADD_HURT", FightPropType.PyroDamageBonus },
        { "FIGHT_PROP_ELEC_ADD_HURT", FightPropType.ElectroDamageBonus },
        { "FIGHT_PROP_WATER_ADD_HURT", FightPropType.HydroDamageBonus },
        { "FIGHT_PROP_GRASS_ADD_HURT", FightPropType.DendroDamageBonus },
        { "FIGHT_PROP_WIND_ADD_HURT", FightPropType.AnemoDamageBonus },
        { "FIGHT_PROP_ROCK_ADD_HURT", FightPropType.GeoDamageBonus },
        { "FIGHT_PROP_ICE_ADD_HURT", FightPropType.CryoDamageBonus },
        { "FIGHT_PROP_MAX_HP", FightPropType.Hp },
        { "FIGHT_PROP_CUR_ATTACK", FightPropType.Attack },
        { "FIGHT_PROP_CUR_DEFENSE", FightPropType.Defense }
    };
}

public enum FightPropType
{
    None = 0,

    BaseHp = 1,
    BaseAttack = 4,
    BaseDefense = 7,
    BaseSpeed = 10,

    HpPercentage = 3,
    AttackPercentage = 6,
    DefensePercentage = 9,
    SpeedPercentage = 11,

    FlatHp = 2,
    FlatAttack = 5,
    FlatDefense = 8,

    CritRate = 20,
    CritDamage = 22,

    EnergyRecharge = 23,
    ElementalMastery = 28,


    HealingBonus = 26,
    IncomingHealingBonus = 27,

    PhysicalResistance = 29,
    PyroResistance = 50,
    ElectroResistance = 51,
    HydroResistance = 52,
    DendroResistance = 53,
    AnemoResistance = 54,
    GeoResistance = 55,
    CryoResistance = 56,

    PhysicalDamageBonus = 30,
    PyroDamageBonus = 40,
    ElectroDamageBonus = 41,
    HydroDamageBonus = 42,
    DendroDamageBonus = 43,
    AnemoDamageBonus = 44,
    GeoDamageBonus = 45,
    CryoDamageBonus = 46,

    PyroEnergyCost = 70,
    ElectroEnergyCost = 71,
    HydroEnergyCost = 72,
    DendroEnergyCost = 73,
    AnemoEnergyCost = 74,
    CryoEnergyCost = 75,
    GeoEnergyCost = 76,
    MaximumSpecialEnergy = 77,
    SpecialEnergyCost = 78,


    CooldownReduction = 80,
    ShieldStrength = 81,

    CurrentPyroEnergy = 1000,
    CurrentElectroEnergy = 1001,
    CurrentHydroEnergy = 1002,
    CurrentDendroEnergy = 1003,
    CurrentAnemoEnergy = 1004,
    CurrentCryoEnergy = 1005,
    CurrentGeoEnergy = 1006,
    CurrentSpecialEnergy = 1007,
    CurrentHp = 1010,

    Hp = 2000,
    Attack = 2001,
    Defense = 2002,
    Speed = 2003,

    ElementalReactionCritRate = 3025,
    ElementalReactionCritDmg = 3026,


    ElementalReactionOverloadedCritRate = 3027,
    ElementalReactionOverloadedCritDmg = 3028,
    ElementalReactionSwirlCritRate = 3029,
    ElementalReactionSwirlCritDmg = 3030,
    ElementalReactionElectroChargedCritRate = 3031,
    ElementalReactionElectroChargedCritDmg = 3032,
    ElementalReactionSuperconductCritRate = 3033,
    ElementalReactionSuperconductCritDmg = 3034,
    ElementalReactionBurnCritRate = 3035,
    ElementalReactionBurnCritDmg = 3036,
    ElementalReactionFrozenShatteredCritRate = 3037,
    ElementalReactionFrozenShatteredCritDmg = 3038,
    ElementalReactionBloomCritRate = 3039,
    ElementalReactionBloomCritDmg = 3040,
    ElementalReactionBurgeonCritRate = 3041,
    ElementalReactionBurgeonCritDmg = 3042,
    ElementalReactionHyperbloomCritRate = 3043,
    ElementalReactionHyperbloomCritDmg = 3044,
    BaseElementalReactionCritRate = 3045,
    BaseElementalReactionCritDmg = 3046
}