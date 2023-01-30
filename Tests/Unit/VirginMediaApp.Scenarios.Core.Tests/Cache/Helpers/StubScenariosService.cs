using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirginMediaApp.Scenarios.Core.Models;
using VirginMediaApp.Scenarios.Core.Services;

namespace VirginMediaApp.Scenarios.Core.Tests.Cache.Helpers;

public class StubScenarioService : IScenariosService
{
    public Task<List<Scenario>> GetScenarios()
    {
        return Task.FromResult(DummyData.Scenarios().ToList());
    }
}