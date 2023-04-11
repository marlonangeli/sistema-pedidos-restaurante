using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Web.Models;

public class GarcomChart
{
    public string[] Labels { get; set; }
    public int[] Data { get; set; }

    public static GarcomChart FromGarcoms(IEnumerable<Garcom> garcoms)
    {
        var chart = new GarcomChart();

        chart.Labels = garcoms.Select(g => $"{g.Nome} {g.Sobrenome}").ToArray();
        chart.Data = garcoms.Select(g => g.Atendimentos.Count).ToArray();

        return chart;
    }
}
