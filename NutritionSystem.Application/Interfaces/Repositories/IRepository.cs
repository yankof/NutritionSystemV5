namespace NutritionSystem.Application.Interfaces.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : EntityBase<TId>
    {
        Task<TEntity?> GetByIdAsync(TId id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }

    // Interfaces específicas para cada entidad principal (si necesitas métodos adicionales)
    public interface IPersonaRepository : IRepository<Persona, Guid> { }
    public interface INutricionistaRepository : IRepository<Nutricionista, Guid> { }
    public interface IPacienteRepository : IRepository<Paciente, Guid> { }
    
    public interface IConsultaRepository : IRepository<Consulta, Guid>
    {
        Task<Consulta?> GetConsultaWithDetailsAsync(Guid id); // Ejemplo de método específico
    }    
    public interface IEvaluacionRepository : IRepository<Evaluacion, Guid> { }
    public interface IDiagnosticoRepository : IRepository<Diagnostico, Guid> { }
    public interface IPlanRepository : IRepository<Plan, Guid> { }
    
    public interface IHistorialPacienteRepository : IRepository<HistorialPaciente, Guid> { }
    public interface IReservaRepository : IRepository<Reserva, Guid> { }
}