namespace NutritionSystem.Domain.Events.Plan;
public record PlanAlimentarioCreado(Guid IdPlanAlimentario, string Nombre, string Tipo, int CantidadDias) : DomainEvent;

