using System;
using System.Collections.Generic;
using System.Linq;
using VirginMediaApp.Scenarios.Core.Models;

namespace VirginMediaApp.Scenarios.Tests.Common;

public class DummyData
{
    public static IEnumerable<Scenario> Scenarios()
    {
        return GetScenarios();
    }

    public static IEnumerable<Scenario> GetScenarios()
    {
        yield return new Scenario { Id = 1, Name = "ABC" };
        yield return new Scenario { Id = 2, Name = "XYZ" };
    }

    public static IEnumerable<Scenario> Scenarios(int ScenarioId)
    {
        return GetScenarios()
            .Where(r => r.ScenarioId == ScenarioId);
    }

    public static IEnumerable<Scenario> GetScenarios()
    {
        yield return new Scenario
        {
            Id = 1,
            DateOfBirth = new DateTime(1991, 1, 1),
            FirstName = "A",
            LastName = "B",
            ScenarioId = 1
        };
        yield return new Scenario
        {
            Id = 2,
            DateOfBirth = new DateTime(1992, 2, 2),
            FirstName = "C",
            LastName = "D",
            ScenarioId = 1
        };

        yield return new Scenario
        {
            Id = 3,
            DateOfBirth = new DateTime(1993, 3, 3),
            FirstName = "E",
            LastName = "F",
            ScenarioId = 2
        };
        yield return new Scenario
        {
            Id = 4,
            DateOfBirth = new DateTime(1994, 4, 4),
            FirstName = "G",
            LastName = "H",
            ScenarioId = 2
        };
    }
}