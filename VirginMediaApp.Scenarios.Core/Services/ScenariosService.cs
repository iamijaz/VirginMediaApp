using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VirginMediaApp.Scenarios.Core.Models;

namespace VirginMediaApp.Scenarios.Core.Services;

// <summary>
// 1. Ideally XMLDataLoader will be running as part of a background service
// 2. dumping results into a db storage
// 3. this service will be loading from that storage
// 4. and will be caching for the configured period of time
// 5. before serving to the UI
// </summary>
public class ScenariosService : IScenariosService
{
    private readonly ILogger<ScenariosService> _logger;
    private readonly IXmlDataLoader _xmlDataLoader;

    public ScenariosService(ILogger<ScenariosService> logger, IXmlDataLoader xmlDataLoader)
    {
        _logger = logger;
        _xmlDataLoader = xmlDataLoader;
    }

    public async Task<List<Scenario>> GetScenarios()
    {
        _logger.LogDebug("Loading data from xml file");
        return _xmlDataLoader.LoadScenarios();
    }
}