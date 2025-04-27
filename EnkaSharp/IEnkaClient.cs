using EnkaSharp.Entities;
using EnkaSharp.Entities.Base;

namespace EnkaSharp;

/// <summary>
/// Represents an abstraction for EnkaClient.
/// </summary>
public interface IEnkaClient
{
    /// <summary>
    /// Provides general abstraction for general Enka API User requests.
    /// </summary>
    public User User { get; }
}