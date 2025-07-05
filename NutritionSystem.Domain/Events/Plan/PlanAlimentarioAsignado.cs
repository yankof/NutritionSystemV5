namespace NutritionSystem.Domain.Events.Plan;
public record PlanAlimentarioAsignado(Guid idContrato, Guid idPlanAlimentario) : DomainEvent;
