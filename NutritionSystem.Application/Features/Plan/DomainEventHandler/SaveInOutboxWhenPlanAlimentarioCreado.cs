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

        var settings = new Newtonsoft.Json.JsonSerializerSettings
        {
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None,
            // Si quieres que el JSON sea legible en la base de datos:
            // Formatting = Newtonsoft.Json.Formatting.Indented
        };
        // Serializa el Content del OutboxMessage a JSON antes de guardarlo
        string serializedContent = Newtonsoft.Json.JsonConvert.SerializeObject(outboxMessage, settings);

        await _outboxService.AddAsync(outboxMessage);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

}
