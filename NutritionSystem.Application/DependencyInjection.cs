using NutritionSystem.Application.Features.Diagnostico.Commands;
using NutritionSystem.Application.Features.Evaluacion.Commands;
using NutritionSystem.Application.Features.Nutricionista.Commands;
using NutritionSystem.Application.Features.Personas.Commands;
using NutritionSystem.Application.Features.Plan.Commands;

namespace NutritionSystem.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            .RegisterServicesFromAssemblies(typeof(CreatePersonaCommand).Assembly)
            .RegisterServicesFromAssembly(typeof(CreateDiagnosticoCommand).Assembly)
            .RegisterServicesFromAssembly(typeof(CreateEvaluacionCommand).Assembly)
            .RegisterServicesFromAssembly(typeof(CreateNutricionistaCommand).Assembly)
            .RegisterServicesFromAssembly(typeof(CreatePlanCommand).Assembly)
            .RegisterServicesFromAssembly(typeof(DomainEvent).Assembly)
        );

        
        return services;
    }
}
