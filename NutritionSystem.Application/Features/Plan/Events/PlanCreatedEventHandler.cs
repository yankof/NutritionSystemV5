namespace NutritionSystem.Application.Features.Plan.Events
{
    public class PlanCreatedEventHandler : INotificationHandler<PlanCreatedEvent>
    {
        private readonly ILogger<PlanCreatedEventHandler> _logger;

        public PlanCreatedEventHandler(ILogger<PlanCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(PlanCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Domain Event: Plan creado: {PlanId} para Consulta {ConsultaId} de tipo {TipoPlan} con descripción '{Descripcion}'. " +
                "Fecha del evento: {Fecha} y status {TipoStatus}",
                notification.PlanId,
                notification.ConsultaId,
                notification.TipoPlan,
                notification.Descripcion,
                notification.DateOccurred,
                notification.TipoStatus);

            // Lógica de negocio secundaria, como notificar al paciente sobre su nuevo plan.

            return Task.CompletedTask;
        }
    }
}