using Joseco.Communication.External.Contracts.Services;
using Joseco.Outbox.Contracts.Model;
using Microsoft.VisualBasic;

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
        //NutritionSystem.Integration.PlanAlimentario.PlanAlimentarioCreado PlanAlimentario = notification.Content.
        //        Select(x => new Integration.Catering.OrdenTrabajoFinalizadoComida(x.IdComida, x.Nombre, x.IdCliente)).ToList();

        NutritionSystem.Integration.PlanAlimentario.PlanAlimentarioCreado message =
            new NutritionSystem.Integration.PlanAlimentario.PlanAlimentarioCreado(Guid.NewGuid(), "1", "Ejercicio", 15);

        await _integrationBusService.PublishAsync(message, "plan-alimentario-creado");
    }
}
