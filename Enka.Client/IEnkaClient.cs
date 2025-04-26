using System.Text.Json;
using Enka.Client.Entities;

namespace Enka.Client;

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