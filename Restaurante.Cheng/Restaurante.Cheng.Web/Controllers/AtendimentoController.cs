using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Enums;
using Restaurante.Cheng.Domain.Entities;
using Bogus;

namespace Restaurante.Cheng.Web.Controllers;

public class AtendimentoController : Controller
{
    private readonly ILogger<AtendimentoController> _logger;
    private readonly IRepository<Atendimento> _atendimentoRepository;
    private readonly IRepository<Produto> _produtoRepository;

    private readonly IRepository<Mesa> _mesaRepository;

    public AtendimentoController(
        ILogger<AtendimentoController> logger,
        IRepository<Atendimento> atendimentoRepository,
        IRepository<Produto> produtoRepository,
        IRepository<Mesa> mesaRepository
    )
    {
        _logger = logger;
        _atendimentoRepository = atendimentoRepository;
        _produtoRepository = produtoRepository;
        _mesaRepository = mesaRepository;
    }

    public IActionResult Index()
    {
        return View();
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
