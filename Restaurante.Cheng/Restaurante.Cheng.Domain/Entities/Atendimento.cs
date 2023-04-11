namespace Restaurante.Cheng.Domain.Entities;

public class Atendimento
{
    public Atendimento()
    {
        Produtos = new HashSet<AtendimentoProduto>();
    }
    
    public int Id { get; set; }
    public Mesa? Mesa { get; set; }
    public int MesaId { get; set; }
    public Garcom? Garcom { get; set; }
    public int GarcomId { get; set; }
    public DateTime HorarioPedido { get; set; }
    public virtual ICollection<AtendimentoProduto> Produtos { get; set; }
}