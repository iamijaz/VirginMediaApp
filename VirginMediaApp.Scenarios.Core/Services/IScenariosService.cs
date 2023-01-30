using System.Collections.Generic;
using System.Threading.Tasks;
using VirginMediaApp.Scenarios.Core.Models;

namespace VirginMediaApp.Scenarios.Core.Services;

public interface IScenariosService
{
    Task<List<Scenario>> GetScenarios();
}