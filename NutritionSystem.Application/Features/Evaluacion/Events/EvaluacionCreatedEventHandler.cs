namespace NutritionSystem.Application.Features.Evaluacion.Events
{
    public class EvaluacionCreatedEventHandler : INotificationHandler<EvaluacionCreatedEvent>
    {
        private readonly ILogger<EvaluacionCreatedEventHandler> _logger;

        public EvaluacionCreatedEventHandler(ILogger<EvaluacionCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(EvaluacionCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Domain Event: Evaluación creada: {EvaluacionId} para Consulta {ConsultaId} de tipo {TipoEvaluacion}. " +
                "Fecha del evento: {Fecha}",
                notification.EvaluacionId,
                notification.ConsultaId,
                notification.TipoEvaluacion,
                notification.DateOccurred);

            // Lógica de negocio secundaria, como enviar una notificación al paciente
            // o actualizar algún estado en un sistema externo.

            return Task.CompletedTask;
        }
    }
}