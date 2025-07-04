using NutritionSystem.Application.Features.Consulta.Commands;
using NutritionSystem.Application.Features.Personas.Commands;
using NutritionSystem.Application.Features.Plan.Commands;
using NutritionSystem.Domain.Entities;
using NutritionSystem.Domain.Enums;

namespace NutritionSystem.Infrastructure.RabbitMQ.Consumers;
public class EvaluacionNutricionalContratadoConsumer : IIntegrationMessageConsumer<EvaluacionNutricionalContratado>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public EvaluacionNutricionalContratadoConsumer(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task HandleAsync(EvaluacionNutricionalContratado message, CancellationToken cancellationToken)
    {
        IEnumerable<Nutricionista> _nutricionistas = await _unitOfWork.Nutricionistas.GetAllAsync();
        var nutricionista = _nutricionistas.FirstOrDefault();
       CreateConsultaCommand _consulta = new CreateConsultaCommand(
            message.idCliente,
            
            nutricionista.Id,            
            DateOnly.FromDateTime(DateTime.UtcNow),
            message.idContrato.ToString()
            );
        var _idConsulta = await _mediator.Send(_consulta, cancellationToken);

        CreatePlanCommand command = new CreatePlanCommand(
            _idConsulta,
            TipoPlan.Dieta,
            TipoStatus.Inicial,
            30, 
            message.idContrato.ToString()
                );

        await _mediator.Send(command, cancellationToken);

    }
}
