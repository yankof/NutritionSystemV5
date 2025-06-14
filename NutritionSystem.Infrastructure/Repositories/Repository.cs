namespace NutritionSystem.Infrastructure.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : EntityBase<TId>
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        private readonly ILogger<Repository<TEntity, TId>> _logger;

        public Repository(ApplicationDbContext dbContext, ILogger<Repository<TEntity, TId>> logger)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _logger = logger;
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            _logger.LogInformation("Fetching {EntityType} by ID: {Id}", typeof(TEntity).Name, id);
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all {EntityType}", typeof(TEntity).Name);
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            _logger.LogInformation("Finding {EntityType} with predicate.", typeof(TEntity).Name);
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            _logger.LogInformation("Adding new {EntityType} with ID: {Id}", typeof(TEntity).Name, entity.Id);
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _logger.LogInformation("Updating {EntityType} with ID: {Id}", typeof(TEntity).Name, entity.Id);
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _logger.LogInformation("Removing {EntityType} with ID: {Id}", typeof(TEntity).Name, entity.Id);
            _dbSet.Remove(entity);
        }
    }

    // Implementaciones específicas para cada repositorio
    public class PersonaRepository : Repository<Persona, Guid>, IPersonaRepository
    {
        public PersonaRepository(ApplicationDbContext dbContext, ILogger<PersonaRepository> logger) : base(dbContext, logger) { }
    }

    public class NutricionistaRepository : Repository<Nutricionista, Guid>, INutricionistaRepository
    {
        public NutricionistaRepository(ApplicationDbContext dbContext, ILogger<NutricionistaRepository> logger) : base(dbContext, logger) { }
    }

    public class PacienteRepository : Repository<Paciente, Guid>, IPacienteRepository
    {
        public PacienteRepository(ApplicationDbContext dbContext, ILogger<PacienteRepository> logger) : base(dbContext, logger) { }
    }

    public class ConsultaRepository : Repository<Consulta, Guid>, IConsultaRepository
    {
        public ConsultaRepository(ApplicationDbContext dbContext, ILogger<ConsultaRepository> logger) : base(dbContext, logger) { }

        public async Task<Consulta?> GetConsultaWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Evaluaciones)
                .Include(c => c.Diagnosticos)
                .Include(c => c.Planes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }

    public class EvaluacionRepository : Repository<Evaluacion, Guid>, IEvaluacionRepository
    {
        public EvaluacionRepository(ApplicationDbContext dbContext, ILogger<EvaluacionRepository> logger) : base(dbContext, logger) { }
    }

    public class DiagnosticoRepository : Repository<Diagnostico, Guid>, IDiagnosticoRepository
    {
        public DiagnosticoRepository(ApplicationDbContext dbContext, ILogger<DiagnosticoRepository> logger) : base(dbContext, logger) { }
    }

    public class PlanRepository : Repository<Plan, Guid>, IPlanRepository
    {
        public PlanRepository(ApplicationDbContext dbContext, ILogger<PlanRepository> logger) : base(dbContext, logger) { }
    }

    public class HistorialPacienteRepository : Repository<HistorialPaciente, Guid>, IHistorialPacienteRepository
    {
        public HistorialPacienteRepository(ApplicationDbContext dbContext, ILogger<HistorialPacienteRepository> logger) : base(dbContext, logger) { }
    }

    public class ReservaRepository : Repository<Reserva, Guid>, IReservaRepository
    {
        public ReservaRepository(ApplicationDbContext dbContext, ILogger<ReservaRepository> logger) : base(dbContext, logger) { }
    }
}