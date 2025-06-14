using Joseco.Outbox.EFCore;
using Joseco.Outbox.EFCore.Persistence;
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

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IOutboxDatabase<DomainEvent>, UnitOfWork>()
            .AddOutbox<DomainEvent>();

        services
            .AddSecrets(configuration)
            .AddRabbitMQ()
            .AddObservability();

        services.AddAplication();

        return services;
    }
}
