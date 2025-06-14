using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.EFCore.Persistence;

namespace NutritionSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IOutboxDatabase<DomainEvent>
    {
        private readonly ApplicationDbContext _dbContext; // Todavía se necesita para CompleteAsync()
        private readonly ILogger<UnitOfWork> _logger; // Logger para la propia UnitOfWork

        // Propiedades de solo lectura que se inicializarán desde el constructor
        public IPersonaRepository Personas { get; }
        public INutricionistaRepository Nutricionistas { get; }
        public IPacienteRepository Pacientes { get; }
        public IConsultaRepository Consultas { get; }
        public IEvaluacionRepository Evaluaciones { get; }
        public IDiagnosticoRepository Diagnosticos { get; }
        public IPlanRepository Planes { get; }
        public IHistorialPacienteRepository HistorialPacientes { get; }
        public IReservaRepository Reservas { get; }

        // El constructor ahora recibe los repositorios ya inyectados
        public UnitOfWork(
            ApplicationDbContext dbContext, // Todavía se necesita para SaveChangesAsync
            ILogger<UnitOfWork> logger,
            IPersonaRepository personas, // <-- Inyectamos cada repositorio
            INutricionistaRepository nutricionistas,
            IPacienteRepository pacientes,
            IConsultaRepository consultas,
            IEvaluacionRepository evaluaciones,
            IDiagnosticoRepository diagnosticos,
            IPlanRepository planes,
            IHistorialPacienteRepository historialPacientes,
            IReservaRepository reservas)
        {
            _dbContext = dbContext;
            _logger = logger;

            // Asignar los repositorios inyectados a las propiedades
            Personas = personas;
            Nutricionistas = nutricionistas;
            Pacientes = pacientes;
            Consultas = consultas;
            Evaluaciones = evaluaciones;
            Diagnosticos = diagnosticos;
            Planes = planes;
            HistorialPacientes = historialPacientes;
            Reservas = reservas;

            _logger.LogInformation("UnitOfWork initialized.");
        }

        public async Task<int> CompleteAsync()
        {
            _logger.LogInformation("Saving all changes to the database.");
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing ApplicationDbContext.");
            _dbContext.Dispose();
        }

        public DbSet<OutboxMessage<DomainEvent>> GetOutboxMessages()
        {
            return _dbContext.OutboxMessages;
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}