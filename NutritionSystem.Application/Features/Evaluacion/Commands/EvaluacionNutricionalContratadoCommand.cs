namespace NutritionSystem.Application.Features.Evaluacion.Commands;
public record EvaluacionNutricionalContratadoCommand(Guid idContrato, Guid idCliente): IRequest<Guid>;
