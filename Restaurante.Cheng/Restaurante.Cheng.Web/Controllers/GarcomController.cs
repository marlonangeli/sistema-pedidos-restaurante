using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Interfaces;
using Restaurante.Cheng.Domain.Enums;
using Restaurante.Cheng.Domain.Entities;
using Bogus;

namespace Restaurante.Cheng.Web.Controllers;

public class GarcomController : Controller
{
    private readonly ILogger<GarcomController> _logger;
    private readonly IRepository<Garcom> _garcomRepository;

    public GarcomController(ILogger<GarcomController> logger, IRepository<Garcom> garcomRepository)
    {
        _logger = logger;
        _garcomRepository = garcomRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        /*
          var garcom = new Garcom 
    {
        Nome = new Bogus.DataSets.Name().FirstName(),
        Sobrenome = new Bogus.DataSets.Name().LastName(),
        NumeroTelefone = new Bogus.DataSets.PhoneNumbers().PhoneNumber()
    };

      await _garcomRepository.AddAsync(garcom);
      */
      

        var garcons = await _garcomRepository.GetAllAsync();
        return View(garcons);
    }

    public IActionResult Edit(int id)
    {
        return PartialView("~/Views/Garcom/Edit.cshtml");
    }

        [Route("Garcom/Create")]
    [HttpPost]
    public async Task<IActionResult> CreateMesaAsync(Garcom garcom)
    {
        var Garcom = await _garcomRepository.AddAsync(garcom);
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
