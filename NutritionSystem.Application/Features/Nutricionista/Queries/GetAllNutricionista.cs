namespace NutritionSystem.Application.Features.Nutricionista.Queries;

public class GetAllNutricionistaQuery : IRequest<IEnumerable<NutricionistaDto>> { }

// Manejador de la Query
public class GetAllNutricionistaQueryHandler : IRequestHandler<GetAllNutricionistaQuery, IEnumerable<NutricionistaDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllNutricionistaQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<NutricionistaDto>> Handle(GetAllNutricionistaQuery request, CancellationToken cancellationToken)
    {
        var nutricionistas = await _unitOfWork.Nutricionistas.GetAllAsync();

        // Mapear la colección de entidades a una colección de DTOs
        return nutricionistas.Select(nutricionista => new NutricionistaDto
        {
            IdNutricionista = nutricionista.Id,
            Titulo = nutricionista.Titulo,
            Activo = nutricionista.Activo.ToString(),
            FechaCreacion = nutricionista.FechaCreacion,
        }).ToList();
    }
}
