using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.EFCore.Persistence;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Immutable;
using System.Threading;


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
        private int result = 0;

        int _transactionCount = 0;
        private readonly IMediator _mediator;
        private readonly IPublisher _publisher;

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
            IReservaRepository reservas,
            IMediator mediator,
            IPublisher publisher)
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
            _mediator = mediator;
            _publisher = publisher;
        }
        
        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Saving all changes to the database.");
            //return await _dbContext.SaveChangesAsync();
            _transactionCount++;

            var domainEvents = _dbContext.ChangeTracker
                .Entries<EntityBase<Guid>>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x =>
                {
                    var domainEvents = x.Entity
                                    .DomainEvents
                                    .ToImmutableArray();
                    x.Entity.ClearDomainEvents();

                    return domainEvents;
                })
                .SelectMany(domainEvents => domainEvents)
                .ToList();

            foreach (var e in domainEvents)
            {
                //await _mediator.Publish(e, cancellationToken);
                await _publisher.Publish(e, cancellationToken);

            }

            if (_transactionCount == 1)
            {
                try
                {
                    result = await _dbContext.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                _transactionCount--;
            }
            return result;
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

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Saving all changes to the database.");
            await _dbContext.SaveChangesAsync();
        }

        
    }
}