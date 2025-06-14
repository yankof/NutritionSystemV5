namespace NutritionSystem.Domain.Entities
{
    // IdPacient en la BD es PK y FK a Persona.
    // Se modela igual que Nutricionista.
    public class Paciente : EntityBase<Guid>
    {
        public EstadoActivo Activo { get; private set; }
        public DateOnly FechaCreacion { get; private set; } // Usando DateOnly

        // Propiedad de navegación para la relación 1-a-1 con Persona
        public Persona Persona { get; private set; }

        // Constructor para EF Core
        private Paciente() { }

        public Paciente(Guid personaId)
        {
            Id = personaId; // La PK de Paciente es el Id de la Persona asociada
            Activo = EstadoActivo.Activo;
            FechaCreacion = DateOnly.FromDateTime(DateTime.UtcNow);
        }

        public void Deactivate()
        {
            Activo = EstadoActivo.Inactivo;
        }

        public void Activate()
        {
            Activo = EstadoActivo.Activo;
        }
    }
}