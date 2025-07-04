namespace NutritionSystem.Application.DTOs;
public class NutricionistaDto
{
    public Guid IdNutricionista { get; set; }
    public string Titulo { get; set; }
    public string Activo { get; set; }
    public DateOnly FechaCreacion { get; set; }

}
