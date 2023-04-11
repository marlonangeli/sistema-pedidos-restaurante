using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
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
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var mesa = await _mesaRepository.GetByIdAsync(id);

        return PartialView("~/Views/Mesa/Edit.cshtml", mesa);
    }

    // [Route("Mesa/CreateMesaAsync")]
    // [HttpPost]
    // public async Task<IActionResult> CreateMesaAsync(Mesa mesa)
    // {
    //     var Mesa = await _mesaRepository.AddAsync(mesa);
    //     return RedirectToAction("Index");
    // }

    [HttpPost]
    public IActionResult Create(Mesa mesa)
    {
        var entity = _mesaRepository.AddAsync(mesa);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return PartialView("~/Views/Mesa/Create.cshtml");
    }

    [Route("Mesa/RemoveMesaAsync/{id}")]
    [HttpPost]
    public async Task<IActionResult> RemoveMesaAsync(int id)
    {
        var mesa = await _mesaRepository.GetByIdAsync(id);
        if (mesa != null) await _mesaRepository.DeleteAsync(mesa);
        else _logger.LogError($"Mesa com id {id} não encontrada");
        return RedirectToAction("Index");
    }

    // public async Task<IActionResult> Edit(int id)
    // {
    //     var mesa = await _mesaRepository.GetByIdAsync(id);
    //     return PartialView("~/Views/Mesa/Edit.cshtml", mesa);
    // }
    
    public async Task<IActionResult> Delete(int id)
    {
        var mesa = await _mesaRepository.GetByIdAsync(id);
        if (mesa != null) await _mesaRepository.DeleteAsync(id);
        else _logger.LogError($"Mesa com id {id} não encontrada");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMesaAsync(Mesa mesa)
    {
        var Mesa = await _mesaRepository.UpdateAsync(mesa);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
