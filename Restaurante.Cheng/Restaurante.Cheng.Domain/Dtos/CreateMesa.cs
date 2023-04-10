using Restaurante.Cheng.Domain.Enums;

namespace Restaurante.Cheng.Domain.Dtos;

public class CreateMesa
{
    public int Numero { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime? HoraAbertura { get; set; }
}