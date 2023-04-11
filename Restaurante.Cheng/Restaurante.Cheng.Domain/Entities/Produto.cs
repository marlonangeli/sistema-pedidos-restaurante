namespace Restaurante.Cheng.Domain.Entities;

public class Produto
{
    public Produto()
    {
        Atendimentos = new HashSet<AtendimentoProduto>();
    }
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public Categoria Categoria { get; set; }
    public int CategoriaId { get; set; }
    public virtual ICollection<AtendimentoProduto> Atendimentos { get; set; }
}