using NutritionSystem.Application.Features.Personas.Commands;
using NutritionSystem.Application.Features.Plan.Commands;
using NutritionSystem.Integration.Cliente;

namespace NutritionSystem.Infrastructure.RabbitMQ.Consumers;
public class ClienteCreadoConsumer : Joseco.Communication.External.RabbitMQ.Services.IIntegrationMessageConsumer<ClienteCreado>
{
    private readonly IMediator _mediator;

    public ClienteCreadoConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task HandleAsync(ClienteCreado message, CancellationToken cancellationToken)
    {
        CrearPersonaCommand command = new CrearPersonaCommand(
                message.Id,
                message.nombre,
                message.apellido,
                "",
                message.email,
                "",
                "",
                message.direccion,
                "",
                ""
                );

        await _mediator.Send(command, cancellationToken);
    }
}