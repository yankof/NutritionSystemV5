//using Joseco.Communication.External.RabbitMQ;
using Joseco.CommunicationExternal.RabbitMQ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nur.Store2025.Observability;
using Nur.Store2025.Observability.Config;
using OpenTelemetry.Trace;


namespace NutritionSystem.Infrastructure.Extensions
{
    public static class ObservabilityExtensions
    {
        public static IServiceCollection AddObservability(this IServiceCollection services, IHostEnvironment environment,
            IConfiguration configuration, string serviceName)
        {
            services.AddScoped<ICorrelationIdProvider, CorrelationIdProvider>();
            var jaegerSettings = services.BuildServiceProvider().GetRequiredService<JeagerSettings>();
            bool isWebApp = environment is IWebHostEnvironment;

            services.AddObservability(serviceName, jaegerSettings,
                builder =>
                {
                    builder.AddSource("Joseco.Outbox")
                        .AddSource("Joseco.Communication.RabbitMQ")
                        .AddSqlClientInstrumentation();
                },
                shouldIncludeHttpInstrumentation: isWebApp);

            if (isWebApp)
            {
                services.AddServicesHealthChecks(configuration);
            }
            return services;
        }

        private static IServiceCollection AddServicesHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services
                .AddHealthChecks()
                .AddSqlServer(connectionString)
                .AddRabbitMqHealthCheck();

            return services;
        }
    }
}