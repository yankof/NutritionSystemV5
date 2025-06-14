
namespace NutritionSystem.Infrastructure.Extensions;
public static class BrokerExtensions
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var rabbitMqSettings = serviceProvider.GetRequiredService<RabbitMqSettings>();

        services.AddRabbitMQ(rabbitMqSettings)
            .AddRabbitMqConsumer<EvaluacionNutricionalContratado, EvaluacionNutricionalContratadoConsumer>("nutricion.evaluacion-nutricional-contratado")
            .AddRabbitMqConsumer<ClienteCreado, ClienteCreadoConsumer>("nutricion.cliente-creado");
            
        return services;
    }

}
