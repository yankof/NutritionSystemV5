namespace NutritionSystem.Application.Features.Nutricionista.Commands
{
    public class CreateNutricionistaCommand : IRequest<Guid>
    {
        public Guid PersonaId { get; set; } // El ID de la persona asociada
        public string Titulo { get; set; }
        public string Turno { get; set; }
    }

    public class CreateNutricionistaCommandHandler : IRequestHandler<CreateNutricionistaCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateNutricionistaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateNutricionistaCommand request, CancellationToken cancellationToken)
        {
            // Validar que la PersonaId existe y no está ya asociada a un nutricionista
            var persona = await _unitOfWork.Personas.GetByIdAsync(request.PersonaId);
            if (persona == null)
            {
                throw new ArgumentException($"La Persona con ID {request.PersonaId} no existe.");
            }
            var existingNutricionista = await _unitOfWork.Nutricionistas.GetByIdAsync(request.PersonaId);
            if (existingNutricionista != null)
            {
                throw new ArgumentException($"La Persona con ID {request.PersonaId} ya está asociada a un nutricionista.");
            }

            var nutricionista = new Domain.Entities.Nutricionista(
                request.PersonaId,
                request.Titulo,
                request.Turno
            );

            await _unitOfWork.Nutricionistas.AddAsync(nutricionista);
            await _unitOfWork.CompleteAsync(); // Esto también disparará el evento de dominio

            return nutricionista.Id;
        }
    }
}