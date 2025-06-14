
namespace NutritionSystem.Infrastructure.Extensions
{
    public static class ObservabilityExtensions
    {
        public static IServiceCollection AddObservability(this IServiceCollection services)//, IHostEnvironment environment)
        {
            services.AddScoped<ICorrelationIdProvider, CorrelationIdProvider>();

            //if (environment is IWebHostEnvironment)
            //{
            //    services.AddServicesHealthChecks();
            //}
            return services;
        }
    }
}