using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VirginMediaApp.Scenarios.Core.Models;
using VirginMediaApp.Scenarios.Core.Services;

namespace VirginMediaApp.Scenarios.Web.Pages.PageModels;

public class IndexPageModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<IndexPageModel> _logger;
    private readonly IScenariosService _scenariosService;

    public IndexPageModel(ILogger<IndexPageModel> logger, IConfiguration configuration, IScenariosService scenariosService)
    {
        _logger = logger;
        _configuration = configuration;
        _scenariosService = scenariosService;
    }

    public string Message { get; set; }

    [BindProperty] public int? CurrentFilter { get; set; }

    public PaginatedList<Scenario> Scenarios { get; set; }


    public async Task<IActionResult> OnPost(int pageIndex)
    {
        _logger.LogDebug($"POST request pageIndex:{pageIndex} ");
        await LoadData(pageIndex);
        return Page();
    }


    public async Task<IActionResult> OnGet(int? pageIndex)
    {
        _logger.LogDebug($"Get request pageIndex:{pageIndex} ");
        return await LoadData(pageIndex);
    }

    private async Task<IActionResult> LoadData( int? pageIndex)
    {

        IEnumerable<Scenario> sortable = await _scenariosService.GetScenarios();

        var pageSize = _configuration.GetValue("PageSize", 4);
        Scenarios = PaginatedList<Scenario>.CreateAsync(sortable.ToList(), pageIndex ?? 1, pageSize);

        return Page();
    }
}