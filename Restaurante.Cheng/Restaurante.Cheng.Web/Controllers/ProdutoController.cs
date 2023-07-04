using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurante.Cheng.Data.Context;

namespace Restaurante.Cheng.Web.Controllers;

public class ProdutoController : Controller
{
    private readonly ILogger<ProdutoController> _logger;
    private readonly IRepository<Produto> _ProdutoRepository;
    private readonly IRepository<Categoria> _CategoriaRepository;

    private readonly IRepository<Atendimento> _atendimentoRepository;
    private readonly RestauranteDbContext _context;

    [ActivatorUtilitiesConstructor]
    public ProdutoController(
        RestauranteDbContext context,
        ILogger<ProdutoController> logger,
        IRepository<Produto> ProdutoRepository,
        IRepository<Categoria> CategoriaRepository,
        IRepository<Atendimento> AtendimentoRepository
    )
    {
        _context = context;
        _logger = logger;
        _ProdutoRepository = ProdutoRepository;
        _CategoriaRepository = CategoriaRepository;
        _atendimentoRepository = AtendimentoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var produtos = await _ProdutoRepository
            .GetQueryable()
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
        if (produto != null)
            await _ProdutoRepository.DeleteAsync(id);
        else
            _logger.LogError($"Produto com id {id} não encontrado");
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

    [HttpGet]
    [Route("Produto/TotalSales")]
    public async Task<IActionResult> GetTotalSales()
    {
        var atendimentos = await _atendimentoRepository.GetAllAsync();
        var vendasPorProduto = atendimentos
            .SelectMany(a => a.Produtos)
            .GroupBy(ap => ap.ProdutoId)
            .Select(
                g =>
                    new
                    {
                        ProdutoNome = g.First().Produto.Nome,
                        QuantidadeVendida = g.Sum(ap => ap.Quantidade)
                    }
            );

        var chartData = new
        {
            labels = vendasPorProduto.Select(v => v.ProdutoNome).ToArray(),
            data = vendasPorProduto.Select(v => v.QuantidadeVendida).ToArray()
        };

        return Ok(chartData);
    }

    [HttpGet]
    [Route("Produto/MostSoldByCategory")]
    public async Task<IActionResult> GetMostSoldByCategory()
    {
        var atendimentos = await _atendimentoRepository.GetAllAsync();
        var vendasPorCategoria = atendimentos
            .SelectMany(a => a.Produtos)
            .GroupBy(ap => ap.Produto.Categoria)
            .Select(
                g =>
                    new
                    {
                        CategoriaNome = g.First().Produto.Categoria.Nome,
                        ProdutoNome = g.OrderByDescending(ap => ap.Quantidade).First().Produto.Nome,
                        QuantidadeVendida = g.Sum(ap => ap.Quantidade)
                    }
            );

        var chartData = new
        {
            labels = vendasPorCategoria.Select(v => v.CategoriaNome).ToArray(),
            data = vendasPorCategoria.Select(v => v.QuantidadeVendida).ToArray(),
            mostSoldByCategory = vendasPorCategoria
                .Select(
                    v =>
                        new
                        {
                            CategoriaNome = v.CategoriaNome,
                            ProdutoNome = v.ProdutoNome,
                            QuantidadeVendida = v.QuantidadeVendida
                        }
                )
                .GroupBy(v => v.CategoriaNome)
                .Select(g => g.OrderByDescending(v => v.QuantidadeVendida).First())
        };

        return Ok(chartData);
    }

    public async Task<IActionResult> Create()
    {
        var categorias = await _CategoriaRepository.GetAllAsync();
        var selectList = new SelectList(categorias, "Id", "Nome");
        ViewBag.Categorias = selectList;
        return PartialView("~/Views/Produto/Create.cshtml");
    }

    /*
        [HttpGet("{atendimentoId}")]
        public async Task<IEnumerable<Produto>> GetAllByAtendimentoIdAsync(int atendimentoId)
        {
            return await _context.AtendimentoProdutos
                .Where(ap => ap.AtendimentoId == atendimentoId)
                .Include(ap => ap.Produto)
                .Select(ap => ap.Produto)
                .ToListAsync();
        }
        */
}
