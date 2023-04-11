namespace Restaurante.Cheng.Domain.Entities;

public class Garcom
{
    public Garcom()
    {
        Atendimentos = new HashSet<Atendimento>();
    }
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string NumeroTelefone { get; set; }

    public virtual ICollection<Atendimento> Atendimentos { get; set; }
}
