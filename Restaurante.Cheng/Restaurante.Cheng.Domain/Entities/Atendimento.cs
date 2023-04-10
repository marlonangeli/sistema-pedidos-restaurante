namespace Restaurante.Cheng.Domain.Entities;

public class Atendimento
{
    public int Id { get; set; }
    public Mesa Mesa { get; set; }
    public int MesaId { get; set; }
    public Garcom Garcom { get; set; }
    public int GarcomId { get; set; }
    public DateTime HorarioPedido { get; set; }
    public List<Produto> Produtos { get; set; }
}