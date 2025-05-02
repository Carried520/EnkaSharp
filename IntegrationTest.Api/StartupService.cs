using EnkaSharp;

namespace IntegrationTest.Api;

public class StartupService : BackgroundService
{
    private readonly IEnkaClient _client;

    public StartupService(IEnkaClient client)
    {
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _client.InitializeAsync();
    }
}