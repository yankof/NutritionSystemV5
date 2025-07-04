namespace NutritionSystem.Application.Features.Personas.Commands
{
    public record CrearPersonaCommand(Guid id, string Nombre, string ApPaterno, string ApMaterno, string Mail, string CI, string Nit,
        string Direccion, string Telefono, string Celular) : IRequest<Guid>;    

    // Manejador del Comando (Lógica de negocio para crear una Persona)
    public class CrearPersonaCommandHandler : IRequestHandler<CrearPersonaCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CrearPersonaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CrearPersonaCommand request, CancellationToken cancellationToken)
        {
            // Crear la entidad de dominio
            var persona = new Persona(
                request.id,
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

            // Agregar la entidad al repositorio
            await _unitOfWork.Personas.AddAsync(persona);
            
            
            var paciente = new Domain.Entities.Paciente(
                request.id
            );

            await _unitOfWork.Pacientes.AddAsync(paciente);
            // Guardar los cambios en la base de datos a través de la Unidad de Trabajo
            await _unitOfWork.CompleteAsync();

            return persona.Id; // Devolver el ID de la nueva persona
        }
    }
}