using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Web.Controllers;

public class GarcomController : Controller
{
    private readonly ILogger<GarcomController> _logger;
    private readonly IRepository<Garcom> _garcomRepository;
    private readonly IRepository<Atendimento> _atendimentoRepository;

    public GarcomController(
        ILogger<GarcomController> logger,
        IRepository<Garcom> garcomRepository,
        IRepository<Atendimento> atendimentoRepository
    )
    {
        _logger = logger;
        _garcomRepository = garcomRepository;
        _atendimentoRepository = atendimentoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var garcons = await _garcomRepository.GetAllAsync();
        return View(garcons);
    }

    [HttpGet]
    [Route("Garcom/SalesByGarcon")]
    public async Task<List<object>> GetSalesByGarcom()
    {
        var atendimentos = await _atendimentoRepository.GetAllAsync();
        var garcons = await _garcomRepository.GetAllAsync();

        var salesByGarcom = garcons.Select(
            garcom =>
                new
                {
                    garcom = $"{garcom.Nome} {garcom.Sobrenome}",
                    total_sales = atendimentos
                        .Where(atendimento => atendimento.GarcomId == garcom.Id)
                        .SelectMany(atendimento => atendimento.Produtos)
                        .Sum(atendimentoProduto => atendimentoProduto.Produto.Preco)
                }
        );

        return salesByGarcom.ToList<object>();
    }

    public async Task<IActionResult> Edit(int id)
    {
        var garcom = await _garcomRepository.GetByIdAsync(id);
        return PartialView("~/Views/Garcom/Edit.cshtml", garcom);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Garcom garcom)
    {
        if (ModelState.IsValid)
        {
            await _garcomRepository.UpdateAsync(garcom);
            return RedirectToAction("Index");
        }

        return View(garcom);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var garcom = await _garcomRepository.GetByIdAsync(id);
        if (garcom != null)
            await _garcomRepository.DeleteAsync(id);
        else
            _logger.LogError($"Garcom com id {id} não encontrado");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Create(Garcom garcom)
    {
        if (ModelState.IsValid)
        {
            await _garcomRepository.AddAsync(garcom);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }

    public IActionResult Create()
    {
        return PartialView("~/Views/Garcom/Create.cshtml");
    }
}
