using System;
using System.Collections.Generic;
using VirginMediaApp.Scenarios.Core.Models;

namespace VirginMediaApp.Scenarios.Core.Tests.Cache.Helpers;

public class DummyData
{
    public static IEnumerable<Scenario> Scenarios()
    {
        return GetScenarios();
    }

    public static IEnumerable<Scenario> GetScenarios()
    {
        yield return new Scenario
        {
            ScenarioId = 1,
            Name = "Scenario1",
            Surname = "BALDWIN",
            Forename = "EDWARD",
            UserId = "6F55DFD1-A235-4BAE-B958-C1A0AB4D5236",
            SampleDate = new DateTime(2013, 02, 01),
            CreationDate = new DateTime(2013, 02, 01),
            NumMonths = 12,
            MarketId = 2,
            NetworkLayerId = 1
        };
        yield return new Scenario
        {
            ScenarioId = 2,
            Name = "Scenario2",
            Surname = "BALDWIN2",
            Forename = "EDWARD2",
            UserId = "ECA4A6AA-72FF-4885-9BEB-5B040FC5EF5C",
            SampleDate = new DateTime(2013, 02, 01),
            CreationDate = new DateTime(2013, 02, 01),
            NumMonths = 12,
            MarketId = 2,
            NetworkLayerId = 1
        };
    }
}