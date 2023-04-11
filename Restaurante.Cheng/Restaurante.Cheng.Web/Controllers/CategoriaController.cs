using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Web.Controllers;

public class CategoriaController : Controller
{
    private readonly ILogger<CategoriaController> _logger;
    private readonly IRepository<Categoria> _CategoriaRepository;

    public CategoriaController(ILogger<CategoriaController> logger, IRepository<Categoria> CategoriaRepository)
    {
        _logger = logger;
        _CategoriaRepository = CategoriaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categorias = await _CategoriaRepository.GetAllAsync();
        return View(categorias);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Categoria categoria)
    {
        if (ModelState.IsValid)
            await _CategoriaRepository.UpdateAsync(categoria);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await _CategoriaRepository.GetByIdAsync(id);
        return PartialView("~/Views/Categoria/Edit.cshtml", categoria);
    }
    
    public IActionResult Create()
    {
        return PartialView("~/Views/Categoria/Create.cshtml");
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            await _CategoriaRepository.AddAsync(categoria);
            return RedirectToAction("Index");
        }

        return View(categoria);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }

    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _CategoriaRepository.GetByIdAsync(id);
        if (categoria != null) await _CategoriaRepository.DeleteAsync(id);
        else _logger.LogError($"Categoria com id {id} não encontrada");
        return RedirectToAction("Index");
    }
}
