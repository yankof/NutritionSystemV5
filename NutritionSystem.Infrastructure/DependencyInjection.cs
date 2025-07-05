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
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment, string serviceName)
    {
        services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

        

        //services.Decorate<IOutboxService<DomainEvent>, OutboxTracingService<DomainEvent>>();
        
        services.AddSecrets(configuration)
            .AddObservability(environment, configuration, serviceName)
            .AddDatabase(configuration)
            .AddRabbitMQ();

        services.AddAplication();

        return services;
    }
}
