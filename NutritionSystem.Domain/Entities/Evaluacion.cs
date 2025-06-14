namespace NutritionSystem.Domain.Entities
{
    public class Evaluacion : EntityBase<Guid>
    {
        public Guid ConsultaId { get; private set; } // FK a Consulta
        public Consulta Consulta { get; private set; }

        public string Descripcion { get; private set; }
        public TipoEvaluacion TipoEvaluacion { get; private set; }
        public DateOnly FechaCreacion { get; private set; }
        public EstadoActivo Activo { get; private set; } // Puede ser un enum EstadoActivo

        private Evaluacion() { }

        public Evaluacion(Guid id, Guid consultaId, string descripcion, TipoEvaluacion tipoEvaluacion)
        {
            Id = id; // Asigna el ID proporcionado o genera uno si es necesario
            ConsultaId = consultaId;
            Descripcion = descripcion;
            TipoEvaluacion = tipoEvaluacion;
            FechaCreacion = DateOnly.FromDateTime(DateTime.UtcNow);
            Activo = EstadoActivo.Activo; // Por defecto activa

            // REGISTRAR EL EVENTO DE DOMINIO:
            AddDomainEvent(new EvaluacionCreatedEvent(this.Id, this.ConsultaId, this.TipoEvaluacion));
        }

        public void UpdateDetails(string newDescripcion, TipoEvaluacion newTipoEvaluacion)
        {
            Descripcion = newDescripcion;
            TipoEvaluacion = newTipoEvaluacion;
            // Podrías añadir un EvaluacionUpdatedEvent aquí
        }

        public void Deactivate()
        {
            Activo = EstadoActivo.Inactivo;
            // Podrías añadir un EvaluacionDeactivatedEvent aquí
        }

        public void Activate()
        {
            Activo = EstadoActivo.Activo;
            // Podrías añadir un EvaluacionActivatedEvent aquí
        }
    }
}