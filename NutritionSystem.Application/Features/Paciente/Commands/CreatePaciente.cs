namespace NutritionSystem.Application.Features.Paciente.Commands
{
    public class CreatePacienteCommand : IRequest<Guid>
    {
        public Guid PersonaId { get; set; } // El ID de la persona asociada
        //public string Ocupacion { get; set; }
        //public string HistoriaClinica { get; set; }
    }

    public class CreatePacienteCommandHandler : IRequestHandler<CreatePacienteCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePacienteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreatePacienteCommand request, CancellationToken cancellationToken)
        {
            // Validar que la PersonaId existe y no está ya asociada a un paciente
            var persona = await _unitOfWork.Personas.GetByIdAsync(request.PersonaId);
            if (persona == null)
            {
                throw new ArgumentException($"La Persona con ID {request.PersonaId} no existe.");
            }
            var existingPaciente = await _unitOfWork.Pacientes.GetByIdAsync(request.PersonaId);
            if (existingPaciente != null)
            {
                throw new ArgumentException($"La Persona con ID {request.PersonaId} ya está asociada a un paciente.");
            }

            var paciente = new Domain.Entities.Paciente(
                request.PersonaId
                //request.Ocupacion,
                //request.HistoriaClinica
            );

            await _unitOfWork.Pacientes.AddAsync(paciente);
            await _unitOfWork.CompleteAsync(); // Esto disparará el evento de dominio

            return paciente.Id;
        }
    }
}