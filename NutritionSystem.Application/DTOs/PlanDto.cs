namespace NutritionSystem.Application.DTOs;
// NutritionSystem.Application/Features/Consulta/Queries/ConsultaDto.cs (Crear este archivo)
public class PlanDto
{
    public Guid Id { get; set; }
    public Guid ConsultaId { get; set; } // FK a Consulta
                                         //public string Consulta { get; set; }
    public string Descripcion { get; set; }
    public TipoPlan TipoPlan { get; set; } // Ejemplo: Nutricional, de Ejercicio, etc.
    public DateOnly FechaCreacion { get; set; }
    //public TipoStatus TipoStatus { get; set; }
    public int DiasTratamiento { get; set; }
    public string IdContrato {get; set; }

}