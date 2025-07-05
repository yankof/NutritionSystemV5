using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;

namespace NutritionSystem.Application.Features.Plan.OutboxMessageHandlers;
public class PublishPlanAlimentarioCreado : INotificationHandler<OutboxMessage<PlanAlimentarioCreado>>
{
    private readonly IExternalPublisher _integrationBusService;

    public PublishPlanAlimentarioCreado(IExternalPublisher integrationBusService)
    {
        _integrationBusService = integrationBusService;
    }
    
    public async Task Handle(OutboxMessage<PlanAlimentarioCreado> notification, CancellationToken cancellationToken)
    {
        NutritionSystem.Integration.PlanAlimentario.PlanAlimentarioCreado message =
            new NutritionSystem.Integration.PlanAlimentario.PlanAlimentarioCreado(notification.Content.IdPlanAlimentario,
            notification.Content.Nombre, notification.Content.Tipo, notification.Content.CantidadDias, notification.Content.IdContrato);


        await _integrationBusService.PublishAsync(message, "plan-alimentario-creado");
    }
}
