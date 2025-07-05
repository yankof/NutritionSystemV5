using Nur.Store2025.Observability.Config;

namespace NutritionSystem.Infrastructure.Extensions;
public static class SecretExtensions
{
    private const string JeagerSettingsSecretName = "JaegerSettings";
    private const string RabbitMqSettingsSecretName = "RabbitMqSettings";
    public static IServiceCollection AddSecrets(this IServiceCollection services, IConfiguration configuration)//, IHostEnvironment environment)
    {
        //string RabbitMqSettingsSecretName = "RabbitMqSettings";


        configuration
            .LoadAndRegister<RabbitMqSettings>(services, RabbitMqSettingsSecretName)
            .LoadAndRegister<JeagerSettings>(services, JeagerSettingsSecretName);

        return services;
     
    }

    private static IConfiguration LoadAndRegister<T>(this IConfiguration configuration, IServiceCollection services,
        string secretName) where T : class, new()
    {
        T secret = Activator.CreateInstance<T>();
        configuration.Bind(secretName, secret);
        services.AddSingleton<T>(secret);
        return configuration;
    }
}
