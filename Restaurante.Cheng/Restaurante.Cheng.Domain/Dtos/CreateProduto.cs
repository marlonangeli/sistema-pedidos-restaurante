using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Domain.Dtos;

public class CreateProduto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
}