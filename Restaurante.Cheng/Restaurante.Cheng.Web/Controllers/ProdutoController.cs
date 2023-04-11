using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        var categorias = await _CategoriaRepository.GetAllAsync();
        var selectList = new SelectList(categorias, "Id", "Nome");
        ViewBag.Categorias = selectList;
        var produto = await _ProdutoRepository.GetByIdAsync(id);
        return PartialView("~/Views/Produto/Edit.cshtml", produto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _ProdutoRepository.GetByIdAsync(id);
        if (produto != null) await _ProdutoRepository.DeleteAsync(id);
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

    [HttpPost]
    public async Task<IActionResult> Edit(Produto produto)
    {
        try
        {
            await _ProdutoRepository.UpdateAsync(produto);
            return RedirectToAction("Index");
        }
        catch
        {
            return View(produto);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Produto produto)
    {
        try
        {
            await _ProdutoRepository.AddAsync(produto);
            return RedirectToAction("Index");
        }
        catch
        {
            return View(produto);
        }
    }

    public async Task<IActionResult> Create()
    {
        var categorias = await _CategoriaRepository.GetAllAsync();
        var selectList = new SelectList(categorias, "Id", "Nome");
        ViewBag.Categorias = selectList;
        return View();
    }
}