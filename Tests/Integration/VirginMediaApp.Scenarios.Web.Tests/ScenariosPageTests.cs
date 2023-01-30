using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using VirginMediaApp.Scenarios.Core.Models;
using VirginMediaApp.Scenarios.Web.Tests.Helpers;
using Xunit;

namespace VirginMediaApp.Scenarios.Web.Tests;

public class ScenariosPageTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _httpClient;

    public ScenariosPageTests(CustomWebApplicationFactory<Startup> factory)
    {
        factory.ClientOptions.BaseAddress = new Uri("http://localhost/");
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task When_loading_home_page_then_it_contains_the_scenarios_data()
    {
        // Arrange
        var scenario = new Scenario
        {
            ScenarioId = 1,
            Name = "Scenario1",
            Surname = "BALDWIN",
            Forename = "EDWARD",
            UserId = "6F55DFD1-A235-4BAE-B958-C1A0AB4D5236",
            SampleDate = DateTime.Parse("2013-02-01T06:02:00"),
            CreationDate = DateTime.Parse("2013-02-01T13:00:00"),
            NumMonths = 12,
            MarketId = 2,
            NetworkLayerId = 1
        };

        // Act
        var response = await _httpClient.GetAsync("/");

        // Assert
        await AssertScenarioRecord(response, scenario);
    }

    private static async Task<IDocument> GetDocumentAsync(HttpResponseMessage response)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();

        var browser = BrowsingContext.New();

        var document = await browser.OpenAsync(virtualResponse =>
        {
            virtualResponse.Content(contentStream, true);
            virtualResponse.Address(response.RequestMessage?.RequestUri).Status(response.StatusCode);
        });

        return document;
    }

    private static async Task AssertScenarioRecord(HttpResponseMessage response, Scenario scenario)
    {
        using var content = await GetDocumentAsync(response);

        var cells = content.QuerySelector<IHtmlTableElement>("table")?.Rows[1].Cells;

        Assert.True(cells[0].TextContent.Contains(scenario.ScenarioId.ToString()),
            "Scenario's ScenarioID is not found");
        Assert.True(cells[1].TextContent.Contains(scenario.Name), "Scenario's Name is not found");
        Assert.True(cells[2].TextContent.Contains(scenario.Surname), "Scenario's Surname is not found");

        Assert.True(cells[3].TextContent.Contains(scenario.Forename), "Scenario's Forename is not found");
        Assert.True(cells[4].TextContent.Contains(scenario.UserId), "Scenario's UserID is not found");
        Assert.True(cells[5].TextContent.Contains(scenario.SampleDate.ToString()),
            "Scenario's SampleDate is not found");

        Assert.True(cells[6].TextContent.Contains(scenario.CreationDate.ToString()),
            "Scenario's CreationDate is not found");
        Assert.True(cells[7].TextContent.Contains(scenario.NumMonths.ToString()), "Scenario's NumMonths is not found");
        Assert.True(cells[8].TextContent.Contains(scenario.MarketId.ToString()), "Scenario's MarketID is not found");

        Assert.True(cells[9].TextContent.Contains(scenario.NetworkLayerId.ToString()),
            "Scenario's NetworkLayerID is not found");
    }
}