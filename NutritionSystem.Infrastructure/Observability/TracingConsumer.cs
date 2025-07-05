using Joseco.Communication.External.Contracts.Message;
using Nur.Store2025.Observability.Tracing;
using System.Diagnostics;

namespace NutritionSystem.Infrastructure.Observability;
public class TracingConsumer<TMessage> : Joseco.Communication.External.RabbitMQ.Services.IIntegrationMessageConsumer<TMessage>
        where TMessage : IntegrationMessage
{
    private readonly ITracingProvider _tracingProvider;
    private readonly Joseco.Communication.External.RabbitMQ.Services.IIntegrationMessageConsumer<TMessage> _decoratedConsumer;

    public TracingConsumer(ITracingProvider tracingProvider, 
        Joseco.Communication.External.RabbitMQ.Services.IIntegrationMessageConsumer<TMessage> decoratedConsumer)
    {
        _tracingProvider = tracingProvider;
        _decoratedConsumer = decoratedConsumer;
    }

    public async Task HandleAsync(TMessage message, CancellationToken cancellationToken)
    {
        var activity = Activity.Current;

        if (activity != null)
        {
            _tracingProvider.SetTraceId(activity.TraceId.ToString());
            _tracingProvider.SetSpanId(activity.SpanId.ToString());
        }
        if (message.CorrelationId != null)
            _tracingProvider.SetCorrelationId(message.CorrelationId);

        await _decoratedConsumer.HandleAsync(message, cancellationToken);

    }
}
