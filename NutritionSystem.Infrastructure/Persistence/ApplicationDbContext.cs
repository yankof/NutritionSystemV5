using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.EFCore.Persistence;
using NutritionSystem.Domain.Common;
using System.Collections.Immutable;

namespace NutritionSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IMediator _mediator; // Inyectamos IMediator

        private int _transactionCount = 0;
        private int result = 0;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Nutricionista> Nutricionistas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<HistorialPaciente> HistorialPacientes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<OutboxMessage<DomainEvent>> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.AddOutboxModel<DomainEvent>();

            // Mapeo de enums (ejemplo de la solución anterior)
            modelBuilder.Entity<Nutricionista>().Property(n => n.Activo).HasConversion<string>();
            modelBuilder.Entity<Paciente>().Property(p => p.Activo).HasConversion<string>();
            modelBuilder.Entity<Consulta>().Property(c => c.Estatus).HasConversion<string>();
            modelBuilder.Entity<Evaluacion>().Property(e => e.TipoEvaluacion).HasConversion<string>();
            modelBuilder.Entity<Evaluacion>().Property(e => e.Activo).HasConversion<string>();
            modelBuilder.Entity<Diagnostico>().Property(d => d.TipoDiagnostico).HasConversion<string>();
            modelBuilder.Entity<Diagnostico>().Property(d => d.TipoStatus).HasConversion<string>();
            
            //modelBuilder.Entity<Plan>().Property(p => p.TipoPlan).HasConversion<string>();
            
            modelBuilder.Entity<Plan>().Property(p => p.TipoStatus).HasConversion<string>();
            modelBuilder.Entity<Reserva>().Property(r => r.Activo).HasConversion<string>();

            // Mapeo de DateOnly (ejemplo de la solución anterior)
            modelBuilder.Entity<Persona>().Property(p => p.FechaCreacion).HasColumnType("date");
            modelBuilder.Entity<Nutricionista>().Property(n => n.FechaCreacion).HasColumnType("date");
            modelBuilder.Entity<Paciente>().Property(p => p.FechaCreacion).HasColumnType("date");
            modelBuilder.Entity<Consulta>().Property(c => c.FechaCreacion).HasColumnType("date");
            modelBuilder.Entity<Evaluacion>().Property(e => e.FechaCreacion).HasColumnType("date");
            modelBuilder.Entity<HistorialPaciente>().Property(hp => hp.FechaCreacion).HasColumnType("date");
            modelBuilder.Entity<Reserva>().Property(r => r.FechaCreacion).HasColumnType("date");
            modelBuilder.Entity<Reserva>().Property(r => r.FechaModificacion).HasColumnType("date");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<DomainEvent>(); // Esta línea es crucial
        }

        // Método para publicar eventos de dominio después de guardar
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            _transactionCount++;
            // Obtener todos los eventos de dominio de las entidades que se están guardando
            //var domainEntities = ChangeTracker.Entries<EntityBase<Guid>>()
            //    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
            //    .Select(x => x.Entity)
            //    .ToList();
            var domainEntities = ChangeTracker.Entries<EntityBase<Guid>>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
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

            foreach (var e in domainEntities)
            {
                await _mediator.Publish(e, cancellationToken);

            }

            if (_transactionCount <= 2)
            {
                // Guardar los cambios de la base de datos primero
                result = await base.SaveChangesAsync(cancellationToken);

            }
            else
            {
                _transactionCount = _transactionCount - 2;
            }

            // Publicar los eventos de dominio solo si los cambios se guardaron con éxito
            //foreach (var entity in domainEntities)
            //{
            //    foreach (var domainEvent in entity.DomainEvents)
            //    {
            //        await _mediator.Publish(domainEvent); // MediatR publica el evento
            //    }
            //    entity.ClearDomainEvents(); // Limpiar los eventos después de publicarlos
            //}
            

            return result;
        }

        // También podrías sobrescribir SaveChanges() de forma síncrona si lo necesitas
        public override int SaveChanges()
        {
            // Lógica similar a SaveChangesAsync
            var domainEntities = ChangeTracker.Entries<EntityBase<Guid>>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();

            var result = base.SaveChanges();

            foreach (var entity in domainEntities)
            {
                foreach (var domainEvent in entity.DomainEvents)
                {
                    // Nota: para eventos asíncronos en SaveChanges síncrono, se puede usar Task.Run
                    _mediator.Publish(domainEvent).GetAwaiter().GetResult();
                }
                entity.ClearDomainEvents();
            }

            return result;
        }
    }
}