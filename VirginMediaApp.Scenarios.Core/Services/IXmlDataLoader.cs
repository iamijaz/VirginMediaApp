using System.Collections.Generic;
using VirginMediaApp.Scenarios.Core.Models;

namespace VirginMediaApp.Scenarios.Core.Services;

public interface IXmlDataLoader
{
    List<Scenario> LoadScenarios();
}