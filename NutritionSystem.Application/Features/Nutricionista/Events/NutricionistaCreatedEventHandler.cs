namespace NutritionSystem.Application.Features.Nutricionista.Events
{
    // INotificationHandler es la interfaz de MediatR para manejar eventos/notificaciones
    public class NutricionistaCreatedEventHandler : INotificationHandler<NutricionistaCreatedEvent>
    {
        private readonly ILogger<NutricionistaCreatedEventHandler> _logger;

        public NutricionistaCreatedEventHandler(ILogger<NutricionistaCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        // Este método se ejecuta cuando se publica un NutricionistaCreatedEvent
        public Task Handle(NutricionistaCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Aquí va la lógica de negocio para reaccionar al evento
            _logger.LogInformation(
                "Domain Event: Nutricionista creado: {NutricionistaId} con título '{Titulo}'. " +
                "Fecha del evento: {Fecha}",
                notification.NutricionistaId,
                notification.Titulo,
                notification.DateOccurred);

            // Ejemplo: Podrías enviar un correo electrónico aquí, o una notificación push
            // await _emailService.SendWelcomeEmail(notification.NutricionistaId);

            return Task.CompletedTask;
        }
    }
}