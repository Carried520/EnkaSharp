using System.Text.Json;
using Enka.Client.Entities;

namespace Enka.Client;

public interface IEnkaClient
{
    public User User { get; }
}