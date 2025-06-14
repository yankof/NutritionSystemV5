namespace NutritionSystem.Domain.Entities
{
    public class HistorialPaciente : EntityBase<Guid>
    {
        public Guid IdPaciente { get; private set; }
        public Guid IdEvaluacion { get; private set; }
        public DateOnly FechaCreacion { get; private set; } // Usando DateOnly
        public string Valor { get; private set; } // nvarchar(1)
        public string Resultado { get; private set; }

        // Propiedades de navegación
        public Paciente Paciente { get; private set; }
        public Evaluacion Evaluacion { get; private set; }

        // Constructor para EF Core
        private HistorialPaciente() { }

        public HistorialPaciente(Guid id, Guid idPaciente, Guid idEvaluacion, string valor, string resultado)
        {
            Id = id;
            IdPaciente = idPaciente;
            IdEvaluacion = idEvaluacion;
            FechaCreacion = DateOnly.FromDateTime(DateTime.UtcNow);
            Valor = valor;
            Resultado = resultado;
        }

        public void Update(string valor, string resultado)
        {
            Valor = valor;
            Resultado = resultado;
        }
    }
}