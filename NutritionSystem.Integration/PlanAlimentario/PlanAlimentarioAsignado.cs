namespace NutritionSystem.Integration.PlanAlimentario;
public record PlanAlimentarioAsignado : IntegrationMessage
{
    public Guid IdContrato { get; set; }
    public Guid idPlanAlimentario { get; set; }

    public PlanAlimentarioAsignado(Guid idContrato, Guid idPlanAlimentario,
        string? correlationId = null, string? source = null) : base(correlationId, source)
    {
        IdContrato = idContrato;
        this.idPlanAlimentario = idPlanAlimentario;

    }
}