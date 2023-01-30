using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using VirginMediaApp.Scenarios.Core.Cache;
using VirginMediaApp.Scenarios.Core.Config;
using VirginMediaApp.Scenarios.Core.Models;
using VirginMediaApp.Scenarios.Core.Services;
using VirginMediaApp.Scenarios.Core.Tests.Cache.Helpers;
using Xunit;

namespace VirginMediaApp.Scenarios.Core.Tests.Cache;

public class CachedScenarioServiceTests
{
    [Fact]
    public async Task When_cache_time_is_set_in_the_config_its_correctly_set_for_the_cached_service()
    {
        // Arrange
        const int expectedMinsToCache = 101;
        var minsToCache = -1;

        var scenariosMock = new Mock<IScenariosService>();
        scenariosMock.Setup(x => x.GetScenarios())
            .ReturnsAsync(DummyData.Scenarios().ToList);

        var cacheMock = new Mock<IDistributedCache>();
        cacheMock.Setup(x => x.TryGetValueAsync<List<Scenario>>(It.IsAny<string>()))
            .ReturnsAsync((false, (List<Scenario>)null));

        cacheMock.Setup(x => x
                .SetAsync(It.IsAny<string>(), It.IsAny<List<Scenario>>(), It.IsAny<int>()))
            .Callback<string, List<Scenario>, int>((key, result, mins) => minsToCache = mins);

        var optionsMock = new Mock<IOptionsMonitor<CacheServicesConfig>>();
        optionsMock.Setup(x => x.Get(CacheServicesConfig.CacheServices))
            .Returns(new CacheServicesConfig { MinsToCache = expectedMinsToCache });

        var sut = new CachedScenariosService(scenariosMock.Object, cacheMock.Object, optionsMock.Object,
            NullLogger<CachedScenariosService>.Instance);


        // Act
        _ = await sut.GetScenarios();

        // Assert
        cacheMock.Verify(x => x.SetAsync(
            It.IsAny<string>(), It.IsAny<List<Scenario>>(), It.IsAny<int>()), Times.Once);

        Assert.Equal(expectedMinsToCache, minsToCache);
    }


    [Fact]
    public async Task When_cache_time_is_set_in_the_config_its_correctly_set_for_the_cached_service_via_stub_services()
    {
        // Arrange
        const int expectedMinsToCache = 101;
        var stubCache = new StubDistributedCache();
        var options = new ServiceCollection()
            .Configure<CacheServicesConfig>(CacheServicesConfig.CacheServices, opt =>
                opt.MinsToCache = expectedMinsToCache)
            .BuildServiceProvider()
            .GetRequiredService<IOptionsMonitor<CacheServicesConfig>>();

        var sut = new CachedScenariosService(new StubScenarioService(), stubCache, options,
            NullLogger<CachedScenariosService>.Instance);

        // Act
        _ = await sut.GetScenarios();

        // Assert
        Assert.True(stubCache.ItemCached);
        Assert.Equal(expectedMinsToCache, stubCache.CachedForMins);
    }
}