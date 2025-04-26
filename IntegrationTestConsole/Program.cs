
using Enka.Client;
using Enka.Client.Entities;

IEnkaClient client = EnkaProviderFactory.Create("Carried-HttpRequest-Test");
EnkaUser user = await client.GetEnkaUserAsync("Algoinde");

Console.WriteLine(user.PlayerInfo.Nickname);