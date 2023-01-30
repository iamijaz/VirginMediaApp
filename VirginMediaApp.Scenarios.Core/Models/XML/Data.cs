using System.Collections.Generic;
using System.Xml.Serialization;

namespace VirginMediaApp.Scenarios.Core.Models.XML;

[XmlRoot(ElementName = "Data")]
public class Data
{
    [XmlElement(ElementName = "Scenario")] public List<Scenario> Scenario { get; set; }
}