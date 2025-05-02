using EnkaSharp.Entities.Base.Raw;

namespace EnkaSharp.Entities.Base.Abstractions;

public interface IEnkaUser
{
    public string? Uid { get; set; }
    public Owner? Owner { get; set; }
}