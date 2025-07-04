namespace NutritionSystem.Application.DTOs;
public class ConsultaDto
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public Guid NutricionistaId { get; set; }
    public DateOnly FechaConsulta { get; set; }
    public string Motivo { get; set; }
    public string Notas { get; set; }
    public string Estatus { get; set; } // Usar string para el DTO
}