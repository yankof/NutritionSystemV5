namespace NutritionSystem.Integration.PlanAlimentario;
public record PlanAlimentarioCreado : IntegrationMessage
{
    public string FullName {  get; set; }
    public Guid IdPlanAlimentario { get; set; }
    public string Nombre { get; set; }
    public string Tipo { get; set; }
    public int CantidadDias { get; set; }

    public PlanAlimentarioCreado(string fullName, Guid idPlanAlimentario, string nombre, string tipo, int cantidadDias, 
        string? correlationId = null, string? source = null) : base(correlationId, source)
    {
        FullName = fullName;
        IdPlanAlimentario = idPlanAlimentario;
        Nombre = nombre;
        Tipo = tipo;
        CantidadDias = cantidadDias;
    }
}
