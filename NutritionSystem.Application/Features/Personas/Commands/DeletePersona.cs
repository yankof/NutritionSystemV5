namespace NutritionSystem.Application.Features.Personas.Commands
{
    public class DeletePersonaCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeletePersonaCommandHandler : IRequestHandler<DeletePersonaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePersonaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = await _unitOfWork.Personas.GetByIdAsync(request.Id);

            if (persona == null)
            {
                return false; // O lanzar una excepción específica
            }

            _unitOfWork.Personas.Remove(persona); // Marcar la entidad para eliminación
            await _unitOfWork.CompleteAsync(); // Guardar cambios

            return true;
        }
    }
}