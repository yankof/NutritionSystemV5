namespace NutritionSystem.Domain.Entities
{
    public class Nutricionista : EntityBase<Guid>
    {
        public string Titulo { get; private set; }
        public string Turno { get; private set; }
        public EstadoActivo Activo { get; private set; }
        public DateOnly FechaCreacion { get; private set; }

        public Persona Persona { get; private set; }

        private Nutricionista() { }

        public Nutricionista(Guid personaId, string titulo, string turno)
        {
            Id = personaId;
            Titulo = titulo;
            Turno = turno;
            Activo = EstadoActivo.Activo;
            FechaCreacion = DateOnly.FromDateTime(DateTime.UtcNow);

            // REGISTRAR EL EVENTO DE DOMINIO:
            // Esto es crucial. Después de crear el nutricionista, registramos que se ha creado.
            AddDomainEvent(new NutricionistaCreatedEvent(this.Id, this.Titulo));
        }

        public void Deactivate()
        {
            if (Activo == EstadoActivo.Activo)
            {
                Activo = EstadoActivo.Inactivo;
                // Opcional: Podrías añadir un evento NutricionistaDeactivatedEvent
            }
        }

        public void Activate()
        {
            if (Activo == EstadoActivo.Inactivo)
            {
                Activo = EstadoActivo.Activo;
                // Opcional: Podrías añadir un evento NutricionistaActivatedEvent
            }
        }

        public void UpdateDetails(string titulo, string turno)
        {
            Titulo = titulo;
            Turno = turno;
            // Opcional: Podrías añadir un evento NutricionistaUpdatedEvent
        }
    }
}