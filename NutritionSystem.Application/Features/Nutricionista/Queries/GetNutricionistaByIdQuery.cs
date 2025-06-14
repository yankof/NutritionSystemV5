namespace NutritionSystem.Application.Features.Nutricionista.Queries;
public class GetNutricionistaByIdQuery:IRequest<NutricionistaDto?>
{
    public Guid Id { get; set; }
}
