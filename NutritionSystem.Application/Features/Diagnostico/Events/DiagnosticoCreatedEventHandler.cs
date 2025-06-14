namespace NutritionSystem.Application.Features.Diagnostico.Events
{
    public class DiagnosticoCreatedEventHandler : INotificationHandler<DiagnosticoCreatedEvent>
    {
        private readonly ILogger<DiagnosticoCreatedEventHandler> _logger;

        public DiagnosticoCreatedEventHandler(ILogger<DiagnosticoCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DiagnosticoCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Domain Event: Diagnóstico creado: {DiagnosticoId} para Consulta {ConsultaId} de tipo {TipoDiagnostico}. " +
                "Fecha del evento: {Fecha} y status {TipoStatus}",
                notification.DiagnosticoId,
                notification.ConsultaId,
                notification.TipoDiagnostico,
                notification.DateOccurred,
                notification.TipoStatus);

            // Lógica de negocio secundaria, como registrar en un historial médico electrónico.

            return Task.CompletedTask;
        }
    }
}