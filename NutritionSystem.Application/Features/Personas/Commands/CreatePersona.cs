namespace NutritionSystem.Application.Features.Personas.Commands
{
    // Comando (Input para la operación de creación)
    public class CreatePersonaCommand : IRequest<Guid>
    {
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
    // Manejador del Comando (Lógica de negocio para crear una Persona)
    public class CreatePersonaCommandHandler : IRequestHandler<CreatePersonaCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePersonaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
        {
            // Crear la entidad de dominio
            var persona = new Persona(
                Guid.Empty,
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

            // Guardar los cambios en la base de datos a través de la Unidad de Trabajo
            await _unitOfWork.CompleteAsync();

            return persona.Id; // Devolver el ID de la nueva persona
        }
    }
}