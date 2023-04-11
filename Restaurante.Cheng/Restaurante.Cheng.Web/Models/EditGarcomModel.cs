using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Web.Models;

public class EditGarcomModel
{
    public int Id { get; set; }
    public Garcom Garcom { get; set; }
    public EditGarcomModel(int id, Garcom garcom)
    {
        this.Id = id;
        this.Garcom = garcom;
    }
}
