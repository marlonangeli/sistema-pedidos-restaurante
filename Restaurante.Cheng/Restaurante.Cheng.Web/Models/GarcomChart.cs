using System.Collections.Generic;
using System.Linq;
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

        return chart;
    }
}
