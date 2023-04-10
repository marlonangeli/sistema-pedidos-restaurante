using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Enums;
using Restaurante.Cheng.Domain.Entities;
using Bogus;

namespace Restaurante.Cheng.Web.Controllers;

public class ProdutoController : Controller
{
    private readonly ILogger<ProdutoController> _logger;
    private readonly IRepository<Produto> _ProdutoRepository;

    public ProdutoController(ILogger<ProdutoController> logger, IRepository<Produto> ProdutoRepository)
    {
        _logger = logger;
        _ProdutoRepository = ProdutoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
            /*
    var faker = new Faker();
var produto = new Produto
{
    Id = faker.Random.Int(),
    Nome = faker.Commerce.ProductName(),
    Descricao = faker.Commerce.ProductDescription(),
    Preco = faker.Random.Decimal(),
    Categoria = new Categoria
    {
        Id = faker.Random.Int(),
        Nome = faker.Commerce.Categories(1)[0],
        Descricao = faker.Lorem.Sentence()
    },
    CategoriaId = faker.Random.Int()
};

      await _ProdutoRepository.AddAsync(produto);
      */
      
      

        var produtos = await _ProdutoRepository.GetAllAsync();
        return View(produtos);
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
