namespace Restaurante.Cheng.Web.Models;
using Restaurante.Cheng.Domain.Entities;

public class MesaIndexModel
{
    public IEnumerable<Mesa> mesa { get; set; }

    public IEnumerable<Atendimento> atendimento { get; set; }

    public MesaIndexModel() { }
}
