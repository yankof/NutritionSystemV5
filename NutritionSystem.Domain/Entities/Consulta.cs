namespace NutritionSystem.Domain.Entities
{
    public class Consulta : EntityBase<Guid>
    {
        public string Descripcion { get; private set; }
        public Guid IdNutricionista { get; private set; }
        public Guid IdPacient { get; private set; }
        public DateOnly FechaCreacion { get; private set; } // Usando DateOnly
        public EstatusConsulta Estatus { get; private set; }

        // Propiedades de navegación
        public Nutricionista Nutricionista { get; private set; }
        public Paciente Paciente { get; private set; }
        public ICollection<Evaluacion> Evaluaciones { get; private set; } = new List<Evaluacion>();
        public ICollection<Diagnostico> Diagnosticos { get; private set; } = new List<Diagnostico>();
        public ICollection<Plan> Planes { get; private set; } = new List<Plan>();

        // Constructor para EF Core
        private Consulta() { }

        public Consulta(Guid id, string descripcion, Guid idNutricionista, Guid idPacient)
        {
            Id = id;
            Descripcion = descripcion;
            IdNutricionista = idNutricionista;
            IdPacient = idPacient;
            FechaCreacion = DateOnly.FromDateTime(DateTime.UtcNow);
            Estatus = EstatusConsulta.Activa;
        }

        public void Update(string descripcion, EstatusConsulta estatus)
        {
            Descripcion = descripcion;
            Estatus = estatus;
        }
    }
}