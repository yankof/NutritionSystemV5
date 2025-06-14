namespace NutritionSystem.Application.Features.Paciente.Events
{
    public class PacienteCreatedEventHandler : INotificationHandler<PacienteCreatedEvent>
    {
        private readonly ILogger<PacienteCreatedEventHandler> _logger;

        public PacienteCreatedEventHandler(ILogger<PacienteCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(PacienteCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Domain Event: Paciente creado: {PacienteId}. " +
                "Fecha del evento: {Fecha}",
                notification.PacienteId,                
                notification.DateOccurred);

            // Lógica de negocio secundaria, como enviar un correo de bienvenida al paciente
            // o notificar a un sistema de gestión de citas.

            return Task.CompletedTask;
        }
    }
}