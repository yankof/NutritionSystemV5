namespace NutritionSystem.Integration.Cliente;
public record ClienteCreado : IntegrationMessage
{
    public Guid id { get; set; }
    public string nombre { get; set; }

    public ClienteCreado(Guid id, string nombre, string? correlationId = null, string? source = null) : base(correlationId, source)
    {
        this.id = id;
        this.nombre = nombre;
    }
}
