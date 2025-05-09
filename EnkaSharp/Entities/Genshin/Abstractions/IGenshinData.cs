using EnkaSharp.Entities.Genshin.Raw;

namespace EnkaSharp.Entities.Genshin.Abstractions;

public interface IGenshinData
{
    public Owner? Owner { get; set; }
}