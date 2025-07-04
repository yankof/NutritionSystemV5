namespace NutritionSystem.Application.Features.Consultas.Queries;
public class GetAllConsultaQuery : IRequest<IEnumerable<ConsultaDto>> { }

// Manejador de la Query
public class GetAllConsultaQueryHandler : IRequestHandler<GetAllConsultaQuery, IEnumerable<ConsultaDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllConsultaQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ConsultaDto>> Handle(GetAllConsultaQuery request, CancellationToken cancellationToken)
    {
        var consultas = await _unitOfWork.Consultas.GetAllAsync();

        // Mapear la colección de entidades a una colección de DTOs
        return consultas.Select(consulta => new ConsultaDto
        {
            Id = consulta.Id,
            PacienteId = consulta.IdPacient,
            NutricionistaId = consulta.IdNutricionista,
            FechaConsulta = consulta.FechaCreacion,
            Motivo = consulta.Descripcion,
            Notas = consulta.Descripcion,
            Estatus = consulta.Estatus.ToString()
        }).ToList();
    }
}
