namespace NutritionSystem.Domain.Events.Plan;
public record PlanAlimentarioCreado(
    string FullName, 
    Guid IdPlanAlimentario, 
    string Nombre, 
    string Tipo, 
    int CantidadDias, 
    Guid IdContrato) : DomainEvent;

