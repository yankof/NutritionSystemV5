namespace NutritionSystem.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no se encontró en 'appsettings.json'.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // IMPORTANTE para el tiempo de diseño:
            // Necesitamos un IMediator. Como no estamos en tiempo de ejecución con DI completa,
            // podemos usar un mock o una implementación vacía.
            // Para esto, instala el paquete NuGet: Moq
            var mediatorMock = new Mock<IMediator>();

            // Puedes configurar el mock si necesitas que haga algo específico,
            // pero para las migraciones, un mock vacío es suficiente.
            // mediatorMock.Setup(m => m.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()))
            //             .Returns(Task.CompletedTask);

            // Pasa el mock de IMediator al constructor del DbContext
            return new ApplicationDbContext(optionsBuilder.Options, mediatorMock.Object);
        }
    }
}