namespace NutritionSystem.Domain.Entities
{
    public class Diagnostico : EntityBase<Guid>
    {
        public Guid ConsultaId { get; private set; } // FK a Consulta
        public Consulta Consulta { get; private set; }

        public string Descripcion { get; private set; }
        public TipoDiagnostico TipoDiagnostico { get; private set; } // Ejemplo: Nutricional, Médico, etc.
        //public DateOnly FechaCreacion { get; private set; }
        public TipoStatus TipoStatus { get; private set; }

        private Diagnostico() { }

        public Diagnostico(Guid id, Guid consultaId, string descripcion, TipoDiagnostico tipoDiagnostico, TipoStatus tipoStatus)
        {
            Id = id; // Asigna el ID proporcionado o genera uno si es necesario
            ConsultaId = consultaId;
            Descripcion = descripcion;
            TipoDiagnostico = tipoDiagnostico;
            //FechaCreacion = DateOnly.FromDateTime(DateTime.UtcNow);
            TipoStatus = tipoStatus;

            // REGISTRAR EL EVENTO DE DOMINIO:
            AddDomainEvent(new DiagnosticoCreatedEvent(this.Id, this.ConsultaId, this.TipoDiagnostico, this.TipoStatus));
        }

        public void UpdateDetails(string newDescripcion, TipoDiagnostico newTipoDiagnostico)
        {
            Descripcion = newDescripcion;
            TipoDiagnostico = newTipoDiagnostico;
            // Podrías añadir un DiagnosticoUpdatedEvent aquí
        }
    }
}