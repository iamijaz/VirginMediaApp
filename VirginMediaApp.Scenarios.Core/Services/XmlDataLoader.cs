using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using VirginMediaApp.Scenarios.Core.Models;
using VirginMediaApp.Scenarios.Core.Models.XML;
using Scenario = VirginMediaApp.Scenarios.Core.Models.Scenario;

namespace VirginMediaApp.Scenarios.Core.Services;

public class XmlDataLoader : IXmlDataLoader
{
    //Todo: A better async alternative
    public List<Scenario> LoadScenarios()
    {
        var xml = $"{AppDomain.CurrentDomain.BaseDirectory}XMLFiles\\data.xml";
        var serializer =
            new XmlSerializer(typeof(Data));

        // Declare an object variable of the type to be deserialized.

        using Stream reader = new FileStream(xml, FileMode.Open);
        // Call the Deserialize method to restore the object's state.
        var deserialize = (Data)serializer.Deserialize(reader);
        var loadScenarios = deserialize?.Scenario.Select(s => new Scenario
            {
                CreationDate = s.CreationDate,
                Forename = s.Forename,
                MarketId = s.MarketId,
                Name = s.Name,
                NetworkLayerId = s.NetworkLayerId,
                NumMonths = s.NumMonths,
                SampleDate = s.SampleDate,
                ScenarioId = s.ScenarioId,
                Surname = s.Surname,
                UserId = s.UserId
            }
        ).ToList();

        return loadScenarios;
    }
}