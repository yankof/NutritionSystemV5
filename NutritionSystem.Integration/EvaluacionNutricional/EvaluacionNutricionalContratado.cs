namespace NutritionSystem.Integration.EvaluacionNutricional;
public record EvaluacionNutricionalContratado : IntegrationMessage
{
    public Guid idContrato { get; set; }
    public Guid idCliente { get; set; }

    public EvaluacionNutricionalContratado(Guid idContrato, Guid idCliente, 
        string? correlationId = null, string? source = null) : base(correlationId, source)
    {
        this.idContrato = idContrato;
        this.idCliente = idCliente;
    }
}
