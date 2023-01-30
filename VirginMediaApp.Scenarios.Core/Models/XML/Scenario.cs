using System;
using System.Xml.Serialization;

namespace VirginMediaApp.Scenarios.Core.Models.XML;

[XmlRoot(ElementName = "Scenario")]
public class Scenario
{
    [XmlElement(ElementName = "ScenarioID")]
    public int ScenarioId { get; set; }

    [XmlElement(ElementName = "Name")] public string Name { get; set; }

    [XmlElement(ElementName = "Surname")] public string Surname { get; set; }

    [XmlElement(ElementName = "Forename")] public string Forename { get; set; }

    [XmlElement(ElementName = "UserID")] public string UserId { get; set; }

    [XmlElement(ElementName = "SampleDate")]
    public DateTime SampleDate { get; set; }

    [XmlElement(ElementName = "CreationDate")]
    public string CreationDateProxy { private get; set; }

    [XmlIgnore] //Todo: better configurable & extendable error handling
    public DateTime CreationDate =>
        string.IsNullOrEmpty(
            CreationDateProxy) ? //Todo: Business question how do you wanna handle the missing values 
            DateTime.MinValue : DateTime.Parse(CreationDateProxy.Replace("\r\n", ""));

    [XmlElement(ElementName = "NumMonths")]
    public int NumMonths { get; set; }

    [XmlElement(ElementName = "MarketID")]
    public int MarketId { get; set; }

    [XmlElement(ElementName = "NetworkLayerID")]
    public int NetworkLayerId { get; set; }
}