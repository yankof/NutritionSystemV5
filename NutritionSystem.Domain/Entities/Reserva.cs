namespace NutritionSystem.Domain.Entities
{
    public class Reserva : EntityBase<Guid>
    {
        public DateOnly FechaCreacion { get; private set; } // Usando DateOnly
        public string HoraCreacion { get; private set; } // varchar(10)
        public EstadoActivo Activo { get; private set; }
        public DateOnly? FechaModificacion { get; private set; } // Usando DateOnly, nullable
        public string HoraModificacion { get; private set; } // varchar(10), nullable

        // Asumo que una reserva es para un paciente con un nutricionista (FKs lógicas)
        public Guid IdNutricionista { get; private set; }
        public Guid IdPacient { get; private set; }

        // Propiedades de navegación
        public Nutricionista Nutricionista { get; private set; }
        public Paciente Paciente { get; private set; }

        // Constructor para EF Core
        private Reserva() { }

        public Reserva(Guid id, Guid idNutricionista, Guid idPacient, DateOnly fechaCreacion, string horaCreacion)
        {
            Id = id;
            IdNutricionista = idNutricionista;
            IdPacient = idPacient;
            FechaCreacion = fechaCreacion;
            HoraCreacion = horaCreacion;
            Activo = EstadoActivo.Activo;
        }

        public void Cancel()
        {
            Activo = EstadoActivo.Inactivo;
            FechaModificacion = DateOnly.FromDateTime(DateTime.UtcNow);
            HoraModificacion = DateTime.UtcNow.ToString("HH:mm");
        }

        public void Reactivate()
        {
            Activo = EstadoActivo.Activo;
            FechaModificacion = DateOnly.FromDateTime(DateTime.UtcNow);
            HoraModificacion = DateTime.UtcNow.ToString("HH:mm");
        }

        public void UpdateDateTime(DateOnly newDate, string newTime)
        {
            FechaCreacion = newDate;
            HoraCreacion = newTime;
            FechaModificacion = DateOnly.FromDateTime(DateTime.UtcNow);
            HoraModificacion = DateTime.UtcNow.ToString("HH:mm");
        }
    }
}