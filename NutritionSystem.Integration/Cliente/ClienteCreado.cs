namespace NutritionSystem.Integration.Cliente;
public record ClienteCreado : IntegrationMessage
{
    public Guid id { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string direccion { get; set; }
    public string email { get;set; }

    public ClienteCreado(Guid id, string nombre, string apellido, string direccion, string email, string? correlationId = null, string? source = null) : base(correlationId, source)
    {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.direccion = direccion;
        this.email = email;

    }
}
