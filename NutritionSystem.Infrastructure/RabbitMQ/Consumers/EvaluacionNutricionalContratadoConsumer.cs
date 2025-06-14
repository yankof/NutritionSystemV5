namespace NutritionSystem.Infrastructure.RabbitMQ.Consumers;
public class EvaluacionNutricionalContratadoConsumer : IIntegrationMessageConsumer<EvaluacionNutricionalContratado>
{
    private readonly IMediator _mediator;

    public EvaluacionNutricionalContratadoConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task HandleAsync(EvaluacionNutricionalContratado message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
