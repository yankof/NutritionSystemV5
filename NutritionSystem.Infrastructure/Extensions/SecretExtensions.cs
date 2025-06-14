namespace NutritionSystem.Infrastructure.Extensions;
public static class SecretExtensions
{
    public static IServiceCollection AddSecrets(this IServiceCollection services, IConfiguration configuration)//, IHostEnvironment environment)
    {
        string RabbitMqSettingsSecretName = "RabbitMqSettings";

        configuration
            .LoadAndRegister<RabbitMqSettings>(services, RabbitMqSettingsSecretName);

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
