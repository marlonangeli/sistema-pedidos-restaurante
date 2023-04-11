using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Enums;
using Restaurante.Cheng.Domain.Entities;
using Bogus;

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

    public IActionResult Edit(int id)
    {
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
