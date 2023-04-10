using Restaurante.Cheng.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Entities;
using Newtonsoft.Json;
using Restaurante.Cheng.Web.Models;

namespace Restaurante.Cheng.Web.Views;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IRepository<Garcom> _garcomRepository;
    public string GarcomChartData { get; set; }

    [BindProperty]
    public List<Garcom> ChartData { get; set; } = default!;

    public IndexModel(ILogger<IndexModel> logger, IRepository<Garcom> garcomRepository)
    {
        _logger = logger;
        _garcomRepository = garcomRepository;
    }

    public async Task OnGetAsync()
    {
        var garcons = await _garcomRepository.GetAllAsync();
        GarcomChartData = JsonConvert.SerializeObject(GarcomChart.FromGarcoms(garcons));
    }
}
