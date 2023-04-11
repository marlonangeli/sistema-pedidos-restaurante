using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Web.Models;

public class EditMesaViewModel
{
    public int Id { get; set; }
    public Mesa Mesa { get; set; }
    public EditMesaViewModel(int id, Mesa mesa)
    {
        this.Id = id;
        this.Mesa = mesa;
    }
}
