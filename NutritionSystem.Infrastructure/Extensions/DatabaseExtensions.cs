using Joseco.Outbox.EFCore;
using Joseco.Outbox.EFCore.Persistence;
using NutritionSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionSystem.Infrastructure.Extensions;
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(context => context.UseSqlServer(connectionString));

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        // --- Inyección de Dependencias ---

        // Repositorios específicos
        services.AddScoped<IPersonaRepository, PersonaRepository>();
        services.AddScoped<INutricionistaRepository, NutricionistaRepository>();
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IConsultaRepository, ConsultaRepository>();
        services.AddScoped<IEvaluacionRepository, EvaluacionRepository>();
        services.AddScoped<IDiagnosticoRepository, DiagnosticoRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IHistorialPacienteRepository, HistorialPacienteRepository>();
        services.AddScoped<IReservaRepository, ReservaRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IOutboxDatabase<DomainEvent>, UnitOfWork>()
            .AddOutbox<DomainEvent>();
            
        return services;
    }
}
