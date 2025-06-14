namespace NutritionSystem.Application.Features.Paciente.Queries
{
    public class GetPacienteByIdQuery : IRequest<PacienteDto>
    {
        public Guid Id { get; set; }
    }

    public class GetPacienteByIdQueryHandler : IRequestHandler<GetPacienteByIdQuery, PacienteDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPacienteByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PacienteDto> Handle(GetPacienteByIdQuery request, CancellationToken cancellationToken)
        {
            // Incluimos la Persona relacionada para obtener sus datos también.
            // Esto requiere que el PacienteRepository tenga un método para incluir Persona
            // O que lo hagas en el QueryHandler (ej. _dbContext.Pacientes.Include(p => p.Persona).FirstOrDefaultAsync(...) )
            var paciente = await _unitOfWork.Pacientes.GetByIdAsync(request.Id);

            if (paciente == null)
            {
                return null;
            }

            // Para obtener la Persona, podrías necesitar otra llamada o cargarla con el Paciente
            // Asumiendo que Pacientes.GetByIdAsync puede cargar la Persona (usando Include en el repo)
            // o la cargas aquí si el repo no lo hace:
            // var persona = await _unitOfWork.Personas.GetByIdAsync(paciente.Id); // Esto es si Paciente.Id == Persona.Id

            return new PacienteDto
            {
                Id = paciente.Id,
                //Ocupacion = paciente.Ocupacion,
                //HistoriaClinica = paciente.HistoriaClinica,
                Activo = paciente.Activo.ToString(),
                // Puedes añadir propiedades de Persona aquí si el DTO las incluye y están cargadas
                // NombrePersona = paciente.Persona?.Nombre,
                // ApPaternoPersona = paciente.Persona?.ApPaterno
            };
        }
    }

    // NutritionSystem.Application/Features/Paciente/Queries/PacienteDto.cs (Crear este archivo)
    public class PacienteDto
    {
        public Guid Id { get; set; }
        //public string Ocupacion { get; set; }
        //public string HistoriaClinica { get; set; }
        public string Activo { get; set; }
        // Agrega aquí cualquier otra propiedad que quieras exponer desde la entidad Persona asociada
        // public string NombrePersona { get; set; }
        // public string ApPaternoPersona { get; set; }
        // public string MailPersona { get; set; }
    }
}