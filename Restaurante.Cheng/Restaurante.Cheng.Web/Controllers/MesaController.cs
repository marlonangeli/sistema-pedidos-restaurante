using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Enums;
using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Web.Controllers;

public class MesaController : Controller
{
    private readonly ILogger<MesaController> _logger;
    private readonly IRepository<Mesa> _mesaRepository;

    public MesaController(ILogger<MesaController> logger, IRepository<Mesa> mesaRepository)
    {
        _logger = logger;
        _mesaRepository = mesaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {

        var mesas = await _mesaRepository.GetAllAsync();
        return View(mesas);
    }

    public IActionResult Edit(int id)
    {
        Console.WriteLine("teste");
        return PartialView("~/Views/Mesa/Edit.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
