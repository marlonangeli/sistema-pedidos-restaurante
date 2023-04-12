namespace Restaurante.Cheng.Domain.Entities;

public class AtendimentoProduto
{
    public int AtendimentoId { get; set; }
    public Atendimento Atendimento { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }

    public int Quantidade { get; set ;}
}