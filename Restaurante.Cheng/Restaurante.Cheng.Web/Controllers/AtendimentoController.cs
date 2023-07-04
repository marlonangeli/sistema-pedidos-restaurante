using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurante.Cheng.Data.Context;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Enums;
using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Web.Controllers;

public class AtendimentoController : Controller
{
    private readonly ILogger<AtendimentoController> _logger;
    private readonly IRepository<Atendimento> _atendimentoRepository;
    private readonly IRepository<Produto> _produtoRepository;
    private readonly IRepository<Garcom> _garcomRepository;
    private readonly IRepository<Mesa> _mesaRepository;
    private readonly RestauranteDbContext _context;

    public AtendimentoController(
        ILogger<AtendimentoController> logger,
        IRepository<Atendimento> atendimentoRepository,
        IRepository<Produto> produtoRepository,
        IRepository<Mesa> mesaRepository, IRepository<Garcom> garcomRepository, 
        RestauranteDbContext context)
    {
        _logger = logger;
        _atendimentoRepository = atendimentoRepository;
        _produtoRepository = produtoRepository;
        _mesaRepository = mesaRepository;
        _garcomRepository = garcomRepository;
        _context = context;
    }

    public async Task<IActionResult> Create()
    {
        var mesas = await _mesaRepository.GetAllAsync();
        ViewBag.Mesas = new SelectList(mesas, nameof(Mesa.Id), nameof(Mesa.Numero));

        var garcons = await _garcomRepository.GetAllAsync();
        ViewBag.Garcons = new SelectList(garcons, nameof(Garcom.Id), nameof(Garcom.Nome));

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Atendimento atendimento)
    {
        await _atendimentoRepository.AddAsync(atendimento);
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> AdicionarProduto(int id)
    {
        var atendimento = await _atendimentoRepository.GetByIdAsync(id);

        var produtos = await _produtoRepository.GetAllAsync();
        ViewBag.Produtos = new SelectList(produtos, nameof(Produto.Id), nameof(Produto.Nome));

        var ap = new AtendimentoProduto()
        {
            AtendimentoId = atendimento.Id
        };
        
        return View("AdicionaProduto", ap);
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarProduto(AtendimentoProduto ap)
    {
        await _context.AtendimentoProdutos.AddAsync(ap);
        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Index()
    {
        var atendimentos = await _atendimentoRepository.GetQueryable()
            .AsNoTracking()
            .Include(i => i.Garcom)
            .Include(i => i.Mesa)
            .ToListAsync();
        
        return View(atendimentos);
    }

    [HttpGet]
    [Route("Atendimento/VisualizarConta/{id}")]
    public async Task<IActionResult> VisualizarConta(int id)
    {
        var atendimento = await _atendimentoRepository.GetQueryable()
            .AsNoTracking()
            .Include(i => i.Garcom)
            .Include(i => i.Mesa)
            .Include(i => i.Produtos)
            .ThenInclude(ap => ap.Produto)
            .FirstOrDefaultAsync(a => a.Id == id);
        
        return View("~/Views/Atendimento/VisualizarConta.cshtml", atendimento);
    }

    [HttpPost]
    [Route("Atendimento/AbrirConta/{mesaId}/{garcomId}")]
    public async Task<IActionResult> AbrirContaAsync(int mesaId, int garcomId)
    {
        var mesa = await _mesaRepository.GetByIdAsync(mesaId);
        /*
        var ultimoAtendimento = mesa.Atendimentos
            .OrderByDescending(a => a.HorarioPedido)
            .FirstOrDefault();
            */
        if (mesa.Status != StatusEnum.Livre)
        {
            return BadRequest("A conta já está aberta para essa mesa.");
        }

        var atendimento = new Atendimento
        {
            MesaId = mesaId,
            GarcomId = garcomId, //
            HorarioPedido = DateTime.Now
        };

        await _atendimentoRepository.AddAsync(atendimento);

        return Ok("Conta aberta com sucesso.");
    }
    /*
      [HttpPost]
      [Route("Atendimento/FecharConta")]
      public async Task<IActionResult> FecharContaAsync(
          int mesaId,
          [FromServices] ProdutoController produtoController
      )
      {

        var ultimoAtendimentoMesa = await _atendimentoRepository
            .GetAllAsync()
            .Where(a => a.MesaId == mesaId)
            .OrderByDescending(a => a.HorarioPedido)
            .FirstOrDefault();

          var lastAtendimento = (Atendimento) await this.GetLastAtendimentoByMesaIdAsync(mesaId);
          if (lastAtendimento == null)
          {
              // A conta não está aberta para essa mesa
              return BadRequest("A conta não está aberta para essa mesa.");
          }
  
          var produtos = await produtoController.GetAllByAtendimentoIdAsync(1);
  
          // Calcular o total da conta
          decimal total = 0;
          foreach (var produto in produtos)
          {
              total += produto.Preco;
          }
          //lastAtendimento.Total = total;
  
          await _atendimentoRepository.UpdateAsync(lastAtendimento);
  
          return Ok(lastAtendimento);
      }
  
      public async Task<IActionResult> GetLastAtendimentoByMesaIdAsync(int mesaId)
      {
          var atendimentos = await _atendimentoRepository.GetAllAsync();
          var filtered = atendimentos.LastOrDefault(a => a.MesaId == mesaId);
          return Ok(filtered);
      }
        */
}