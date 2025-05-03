using System.Reflection;

namespace EnkaSharp.AssetHandlers;

internal class AssetDispatcher
{
    private readonly Dictionary<GameType, IAssetHandler> _assetHandlers = new();

    internal AssetDispatcher()
    {
        IEnumerable<Type> handlerTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IAssetHandler).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false });

        foreach (Type type in handlerTypes)
        {
            var instance = (IAssetHandler)Activator.CreateInstance(type)!;
            _assetHandlers.Add(instance.GameType, instance);
        }
    }

    internal IAssetHandler this[GameType index]
    {
        get
        {
            if (!EnkaClient.IsInitialized)
                throw new InvalidOperationException("Enka Client is not initialized!");
            return _assetHandlers[index];
        }
    }

    internal async Task InitAsync()
    {
        var list = new List<Task>();
        foreach ((GameType key, IAssetHandler? handler) in _assetHandlers)
        {
            list.Add(handler.DownloadDataAsync());
        }

        await Task.WhenAll(list);
    }
}