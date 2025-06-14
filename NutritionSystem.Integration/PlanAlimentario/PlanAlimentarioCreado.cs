using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionSystem.Integration.PlanAlimentario;
public record PlanAlimentarioCreado : IntegrationMessage
{
    public Guid IdPlanAlimentario { get; set; }
    public string Nombre { get; set; }
    public string Tipo { get; set; }
    public int CantidadDias { get; set; }

    public PlanAlimentarioCreado(Guid idPlanAlimentario, string nombre, string tipo, int cantidadDias, 
        string? correlationId = null, string? source = null) : base(correlationId, source)
    {
        IdPlanAlimentario = idPlanAlimentario;
        Nombre = nombre;
        Tipo = tipo;
        CantidadDias = cantidadDias;
    }
}
