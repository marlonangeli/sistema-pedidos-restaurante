using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Web.Controllers;

public class MesaController : Controller
{
    private readonly ILogger<MesaController> _logger;
    private readonly IRepository<Mesa> _mesaRepository;
    private readonly IRepository<Atendimento> _atendimentoRepository;

    public MesaController(ILogger<MesaController> logger, IRepository<Mesa> mesaRepository, IRepository<Atendimento> atendimentoRepository)
    {
        _logger = logger;
        _mesaRepository = mesaRepository;
        _atendimentoRepository = atendimentoRepository;
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
    
    [HttpGet]
    public async Task<IActionResult> GetSalesByMesa()
    {
        var atendimentos = await _atendimentoRepository.GetAllAsync();
        var salesByMesa = atendimentos
            .GroupBy(a => a.MesaId)
            .Select(
                g =>
                    new
                    {
                        mesaId = g.Key,
                        totalSales = g.Sum(a => a.Produtos.Sum(ap => ap.Produto.Preco))
                    }
            );

        return Ok(salesByMesa.ToList<object>());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }

    public async Task<IActionResult> CloseBill(int mesaId)
    {
        var mesa = await _mesaRepository.GetByIdAsync(mesaId);
        if (mesa != null)
        {
            mesa.Atendimentos.Clear();
            await _mesaRepository.UpdateAsync(mesa);
        }
        else _logger.LogError($"Mesa com id {mesaId} não encontrada");
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> ViewBill(int id)
    {
        var mesa = await _mesaRepository.GetQueryable()
            .AsNoTracking()
            .Include(i => i.Atendimentos)
            .ThenInclude(i => i.Garcom)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        var valorTotal = mesa.Atendimentos.Sum(a => a.Produtos.Sum(ap => ap.Produto.Preco));
        var garcom = mesa.Atendimentos.Select(s => s.Garcom.Nome).FirstOrDefault();
        ViewBag.ValorTotal = valorTotal;
        ViewBag.Garcom = garcom;
        return View(mesa);
    }
}
