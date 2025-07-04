using Joseco.Communication.External.Contracts.Services;

namespace NutritionSystem.Application.Features.Plan.OutboxMessageHandlers;
public class PublishPlanAlimentarioAsignado : INotificationHandler<OutboxMessage<PlanAlimentarioAsignado>>
{
    private readonly IExternalPublisher _integrationBusService;

    public PublishPlanAlimentarioAsignado(IExternalPublisher integrationBusService)
    {
        _integrationBusService = integrationBusService;
    }

    public async Task Handle(OutboxMessage<PlanAlimentarioAsignado> notification, CancellationToken cancellationToken)
    {
        NutritionSystem.Integration.PlanAlimentario.PlanAlimentarioAsignado message =
            new NutritionSystem.Integration.PlanAlimentario.PlanAlimentarioAsignado(notification.Content.idContrato,
            notification.Content.IdPlanAlimentario);

        await _integrationBusService.PublishAsync(message, "plan-alimentario-asignado");
    }
}
