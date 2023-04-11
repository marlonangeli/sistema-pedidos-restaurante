using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Enums;
using Restaurante.Cheng.Domain.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Cheng.Web.Controllers;

public class GarcomController : Controller
{
    private readonly ILogger<GarcomController> _logger;
    private readonly IRepository<Garcom> _garcomRepository;
    private readonly IRepository<Atendimento> _atendimentoRepository;

    public GarcomController(ILogger<GarcomController> logger, IRepository<Garcom> garcomRepository,
        IRepository<Atendimento> atendimentoRepository)
    {
        _logger = logger;
        _garcomRepository = garcomRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var garcons = await _garcomRepository.GetAllAsync();
        return View(garcons);
    }

    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.Atendimentos = await _atendimentoRepository.GetQueryable()
            .AsNoTracking()
            .Include(i => i.Mesa)
            .ToListAsync();
        var garcom = await _garcomRepository.GetByIdAsync(id);
        return PartialView("~/Views/Garcom/Edit.cshtml", garcom);
    }

    // [HttpPost]
    // public async Task<IActionResult> Edit(int id, Garcom garcom)
    // {
    //     // [Bind("Id,Nome,Sobrenome,NumeroTelefone,AtendimentoId")]
    //     if (ModelState.IsValid)
    //     {
    //         await _garcomRepository.UpdateAsync(garcom);
    //         return RedirectToAction("Index");
    //     }
    //
    //     return View(garcom);
    // }
    
    public async Task<IActionResult> Delete(int id)
    {
        var garcom = await _garcomRepository.GetByIdAsync(id);
        if (garcom != null) await _garcomRepository.DeleteAsync(garcom);
        else _logger.LogError($"Garcom com id {id} não encontrado");
        return RedirectToAction("Index");
    }
    
    [Route("Garcom/Create")]
    [HttpPost]
    public async Task<IActionResult> CreateMesaAsync(Garcom garcom)
    {
        var Garcom = await _garcomRepository.AddAsync(garcom);
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
