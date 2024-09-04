using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OtoMotoWebFlowSync.Services;

public class SynchronizationService : ISynchronizationService
{
    private readonly ILogger<SynchronizationService> _logger;

    public SynchronizationService(ILogger<SynchronizationService> logger)
    {
        _logger = logger;
    }

    public async Task Run()
    {
        
    }

}

public interface ISynchronizationService
{
    Task Run();
}