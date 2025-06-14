namespace NutritionSystem.Application.Features.Personas.Commands
{
    public class UpdatePersonaCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Mail { get; set; }
        public string CI { get; set; }
        public string Nit { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
    }

    public class UpdatePersonaCommandHandler : IRequestHandler<UpdatePersonaCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePersonaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdatePersonaCommand request, CancellationToken cancellationToken)
        {
            var persona = await _unitOfWork.Personas.GetByIdAsync(request.Id);

            if (persona == null)
            {
                return false; // O lanzar una excepción específica de "no encontrado"
            }

            // Actualizar la entidad de dominio usando su método de comportamiento
            persona.Update(
                request.Nombre,
                request.ApPaterno,
                request.ApMaterno,
                request.Mail,
                request.CI,
                request.Nit,
                request.Direccion,
                request.Telefono,
                request.Celular
            );

            _unitOfWork.Personas.Update(persona); // Marcar la entidad como modificada
            await _unitOfWork.CompleteAsync(); // Guardar cambios

            return true;
        }
    }
}