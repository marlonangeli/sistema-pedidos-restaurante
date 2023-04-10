using Restaurante.Cheng.Domain.Enums;

namespace Restaurante.Cheng.Domain.Entities;

public class Mesa
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime? HoraAbertura { get; set; }
}