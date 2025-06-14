using MediatR;
using NutritionSystem.Application.Abstraction;
using Joseco.Outbox.Contracts.Model;
using Joseco.Outbox.Contracts.Service;

namespace NutritionSystem.Application.Features.Plan.DomainEventHandler;

public class SaveInOutboxWhenPlanAlimentarioCreado : INotificationHandler<PlanAlimentarioCreado>
{
    private readonly IOutboxService<DomainEvent> _outboxService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public SaveInOutboxWhenPlanAlimentarioCreado(IOutboxService<DomainEvent> outboxService, IUnitOfWork unitOfWork, 
        ICorrelationIdProvider correlationIdProvider)
    {
        _outboxService = outboxService;
        _unitOfWork = unitOfWork;
        _correlationIdProvider = correlationIdProvider;
    }

    public async Task Handle(PlanAlimentarioCreado notification, CancellationToken cancellationToken)
    {
        var correlationId = _correlationIdProvider.GetCorrelationId();
        OutboxMessage<DomainEvent> outboxMessage = new(notification);

        await _outboxService.AddAsync(outboxMessage);
        await _unitOfWork.CompleteAsync();
    }
}
