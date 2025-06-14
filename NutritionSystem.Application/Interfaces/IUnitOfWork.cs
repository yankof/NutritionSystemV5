namespace NutritionSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Propiedades para acceder a los repositorios específicos
        IPersonaRepository Personas { get; }
        INutricionistaRepository Nutricionistas { get; }
        IPacienteRepository Pacientes { get; }
        IConsultaRepository Consultas { get; }
        IEvaluacionRepository Evaluaciones { get; }
        IDiagnosticoRepository Diagnosticos { get; }
        IPlanRepository Planes { get; }
        IHistorialPacienteRepository HistorialPacientes { get; }
        IReservaRepository Reservas { get; }

        Task<int> CompleteAsync(); // Guarda todos los cambios pendientes en la base de datos
    }
}
