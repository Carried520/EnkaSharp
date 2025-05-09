using EnkaSharp.Entities.Base.Raw;

namespace EnkaSharp.Entities.Genshin.Raw;

public class Owner
{
    public string? Hash { get; set; }
    public string? Username { get; set; }
    public Profile? Profile { get; set; }
    public int Id { get; set; }
}