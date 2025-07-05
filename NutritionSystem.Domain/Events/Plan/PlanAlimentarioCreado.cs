namespace NutritionSystem.Domain.Events.Plan;
public record PlanAlimentarioCreado(
    Guid IdPlanAlimentario,
    //Guid PlanId,
    string Nombre, 
    string Tipo, 
    int CantidadDias,
    Guid IdContrato
    ) : DomainEvent;

