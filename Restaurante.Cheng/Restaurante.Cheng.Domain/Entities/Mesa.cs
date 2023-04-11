using Restaurante.Cheng.Domain.Enums;

namespace Restaurante.Cheng.Domain.Entities;

public class Mesa
{
    public Mesa()
    {
        Atendimentos = new HashSet<Atendimento>();
    }
    public int Id { get; set; }
    public int Numero { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime? HoraAbertura { get; set; }
    public virtual ICollection<Atendimento> Atendimentos { get; set; }
}