using System.Globalization;
using EnkaSharp.Entities.Base.Abstractions;
using EnkaSharp.Entities.Base.Raw;

namespace EnkaSharp.Mappers;

internal static class PropMapMapper
{
    internal static Dictionary<PropMapNodeType, int> MapPropMap(Dictionary<string, PropMapNode> propMapNodes)
    {
        var dictOfStats = new Dictionary<PropMapNodeType, int>();
        foreach ((string? _, PropMapNode? node) in propMapNodes)
        {
            if (!Enum.IsDefined(typeof(PropMapNodeType), node.Type))
            {
                continue;
            }
            
            var strongType = (PropMapNodeType)node.Type;
            dictOfStats[strongType] = string.IsNullOrEmpty(node.Val) ? 0 : int.Parse(node.Val);
        }

        return dictOfStats;
    }

    internal static Dictionary<FightPropType, double> MapFightProps(Dictionary<string, double> fightMapNodes)
    {
        var dictOfStats = new Dictionary<FightPropType, double>();
        foreach ((string key, double value) in fightMapNodes)
        {
            int numberKey = int.Parse(key);
            if (!Enum.IsDefined(typeof(FightPropType), numberKey))
            {
                continue;
            }


            var strongType = (FightPropType)numberKey;
            double calculatedPercentage = HandlePercentage(strongType, value);
            dictOfStats[strongType] = calculatedPercentage;
        }

        return dictOfStats;
    }

    private static double HandlePercentage(FightPropType fightPropType, double value)
    {
        var type = fightPropType.ToString();
        return type.Contains("Percentage") || type.Contains("Bonus") || type.Contains("Percentage") ||
               type.Contains("Crit") ||
               fightPropType == FightPropType.EnergyRecharge ||
               fightPropType is FightPropType.CooldownReduction
            ? double.Parse((value * 100).ToString("F1", CultureInfo.InvariantCulture) , CultureInfo.InvariantCulture)
            : double.Parse(value.ToString("N0", CultureInfo.InvariantCulture) , CultureInfo.InvariantCulture);
    }
}