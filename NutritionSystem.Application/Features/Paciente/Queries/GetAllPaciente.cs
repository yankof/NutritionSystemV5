namespace NutritionSystem.Application.Features.Paciente.Queries;
public class GetAllPacienteQuery : IRequest<IEnumerable<PacienteDto>> { }

// Manejador de la Query
public class GetAllPacienteQueryHandler : IRequestHandler<GetAllPacienteQuery, IEnumerable<PacienteDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPacienteQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PacienteDto>> Handle(GetAllPacienteQuery request, CancellationToken cancellationToken)
    {
        var pacientes = await _unitOfWork.Pacientes.GetAllAsync();

        // Mapear la colección de entidades a una colección de DTOs
        return pacientes.Select(paciente => new PacienteDto
        {
            Id = paciente.Id,
            Activo = paciente.Activo.ToString(),
        }).ToList();
    }
}
