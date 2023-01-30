using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VirginMediaApp.Scenarios.Core.Config;
using VirginMediaApp.Scenarios.Core.Models;
using VirginMediaApp.Scenarios.Core.Services;

namespace VirginMediaApp.Scenarios.Core.Cache;

public class CachedScenariosService : IScenariosService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CachedScenariosService> _logger;
    private readonly int _minsToCache;
    private readonly IScenariosService _scenariosService;

    public CachedScenariosService(IScenariosService scenariosService,
        IDistributedCache cache, IOptionsMonitor<CacheServicesConfig> options,
        ILogger<CachedScenariosService> logger)
    {
        _scenariosService = scenariosService;
        _cache = cache;
        _logger = logger;
        _minsToCache = options.Get(CacheServicesConfig.CacheServices).MinsToCache;
    }

    public async Task<List<Scenario>> GetScenarios()
    {
        return await GetCachedScenarios(() => _scenariosService.GetScenarios());
    }

    private async Task<List<TU>> GetCachedScenarios<TU>(Func<Task<List<TU>>> getAll)
    {
        var cacheKey = $"current_scenarios_{DateTime.UtcNow:yyyy_MM_dd}";

        var (isCached, scenarios) = await _cache.TryGetValueAsync<List<TU>>(cacheKey);
        if (isCached)
        {
            _logger.LogDebug($"Cached version for {cacheKey} is found");
            return scenarios;
        }

        _logger.LogDebug($"Creating a new cached version for {cacheKey}");

        var result = await getAll();

        if (result != null)
            await _cache.SetAsync(cacheKey, result, _minsToCache);

        return result;
    }
}