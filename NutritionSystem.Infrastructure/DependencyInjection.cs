using Joseco.Outbox.Contracts.Service;
using Joseco.Outbox.EFCore;
using Joseco.Outbox.EFCore.Persistence;
using Microsoft.Extensions.Hosting;
using NutritionSystem.Application;
using NutritionSystem.Infrastructure.Extensions;
using NutritionSystem.Infrastructure.Repositories;

namespace NutritionSystem.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

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

        //services.Decorate<IOutboxService<DomainEvent>, OutboxTracingService<DomainEvent>>();

        services.AddSecrets(configuration)
            .AddObservability()            
            .AddRabbitMQ();

        services.AddAplication();

        return services;
    }
}
