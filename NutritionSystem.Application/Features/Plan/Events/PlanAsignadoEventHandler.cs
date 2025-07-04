namespace NutritionSystem.Application.Features.Plan.Events;
public class PlanAsignadoEventHandler : INotificationHandler<PlanAsignadoEvent>
{
    private readonly ILogger<PlanAsignadoEventHandler> _logger;

    public PlanAsignadoEventHandler(ILogger<PlanAsignadoEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PlanAsignadoEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Domain Event: Plan creado: {PlanId} para contrato {ContratoId}",
            notification.ContratoId,
            notification.PlanId);
            

        // Lógica de negocio secundaria, como notificar al paciente sobre su nuevo plan.

        return Task.CompletedTask;
    }
}