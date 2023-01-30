using System;

namespace VirginMediaApp.Scenarios.Core.Models;

public class Scenario
{
    public int ScenarioId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Forename { get; set; }

    public string UserId { get; set; }

    public DateTime SampleDate { get; set; }
    public DateTime CreationDate { get; set; }

    public int NumMonths { get; set; }

    public int MarketId { get; set; }
    public int NetworkLayerId { get; set; }
}