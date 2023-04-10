using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Domain.Dtos;

public class CreateAtendimento
{
    public int MesaId { get; set; }
    public int GarcomId { get; set; }
    public DateTime HorarioPedido { get; set; }
    public int[]? Produtos { get; set; }
}