namespace NutritionSystem.Application.Features.Nutricionista.Queries;
public class GetNutricionistaByIdQuery:IRequest<NutricionistaDto?>
{
    public Guid Id { get; set; }
}
public class GetNutricionistaByIdQueryHandler : IRequestHandler<GetNutricionistaByIdQuery, NutricionistaDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetNutricionistaByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<NutricionistaDto> Handle(GetNutricionistaByIdQuery request, CancellationToken cancellationToken)
    {
        // Incluimos la Persona relacionada para obtener sus datos también.
        // Esto requiere que el PacienteRepository tenga un método para incluir Persona
        // O que lo hagas en el QueryHandler (ej. _dbContext.Pacientes.Include(p => p.Persona).FirstOrDefaultAsync(...) )
        var nutricionista = await _unitOfWork.Nutricionistas.GetByIdAsync(request.Id);

        if (nutricionista == null)
        {
            return null;
        }

        // Para obtener la Persona, podrías necesitar otra llamada o cargarla con el Paciente
        // Asumiendo que Pacientes.GetByIdAsync puede cargar la Persona (usando Include en el repo)
        // o la cargas aquí si el repo no lo hace:
        // var persona = await _unitOfWork.Personas.GetByIdAsync(paciente.Id); // Esto es si Paciente.Id == Persona.Id

        return new NutricionistaDto
        {
            IdNutricionista = nutricionista.Id,
            Titulo = nutricionista.Titulo,
            Activo = nutricionista.Activo.ToString(),
            FechaCreacion = nutricionista.FechaCreacion,
        };
    }
}