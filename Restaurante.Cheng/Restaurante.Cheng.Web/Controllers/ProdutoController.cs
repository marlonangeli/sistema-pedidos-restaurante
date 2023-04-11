using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Enums;
using Restaurante.Cheng.Domain.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Cheng.Web.Controllers;

public class ProdutoController : Controller
{
    private readonly ILogger<ProdutoController> _logger;
    private readonly IRepository<Produto> _ProdutoRepository;
    private readonly IRepository<Categoria> _CategoriaRepository;

    public ProdutoController(ILogger<ProdutoController> logger, IRepository<Produto> ProdutoRepository,
        IRepository<Categoria> CategoriaRepository)
    {
        _logger = logger;
        _ProdutoRepository = ProdutoRepository;
        _CategoriaRepository = CategoriaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var produtos = await _ProdutoRepository.GetQueryable()
            .AsNoTracking()
            .Include(i => i.Categoria)
            .ToListAsync();
        return View(produtos);
    }

    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.Categorias = await _CategoriaRepository.GetAllAsync();
        var produto = await _ProdutoRepository.GetByIdAsync(id);
        return PartialView("~/Views/Produto/Edit.cshtml", produto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _ProdutoRepository.GetByIdAsync(id);
        if (produto != null) await _ProdutoRepository.DeleteAsync(produto);
        else _logger.LogError($"Produto com id {id} não encontrado");
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