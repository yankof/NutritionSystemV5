using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionSystem.Application.Features.Plan.DomainEventHandler;
public class SaveInOutboxWhenPlanAlimentarioAsignado : INotificationHandler<PlanAlimentarioAsignado>
{
    private readonly IOutboxService<DomainEvent> _outboxService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public SaveInOutboxWhenPlanAlimentarioAsignado(IOutboxService<DomainEvent> outboxService, IUnitOfWork unitOfWork,
        ICorrelationIdProvider correlationIdProvider)
    {
        _outboxService = outboxService;
        _unitOfWork = unitOfWork;
        _correlationIdProvider = correlationIdProvider;
    }

    public async Task Handle(PlanAlimentarioAsignado notification, CancellationToken cancellationToken)
    {
        var correlationId = _correlationIdProvider.GetCorrelationId();
        OutboxMessage<DomainEvent> outboxMessage = new(notification);

        await _outboxService.AddAsync(outboxMessage);
        await _unitOfWork.CompleteAsync();
    }
}

