namespace NutritionSystem.Application.Features.Personas.Queries
{
    // Query (Input para la operación de lectura por ID)
    public class GetPersonaByIdQuery : IRequest<PersonaDto?>
    {
        public Guid Id { get; set; }
    }

    // Manejador de la Query (Lógica de lectura)
    public class GetPersonaByIdQueryHandler : IRequestHandler<GetPersonaByIdQuery, PersonaDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPersonaByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PersonaDto?> Handle(GetPersonaByIdQuery request, CancellationToken cancellationToken)
        {
            var persona = await _unitOfWork.Personas.GetByIdAsync(request.Id);

            if (persona == null)
            {
                return null;
            }

            // Mapear la entidad de dominio a un DTO (Data Transfer Object)
            // Esto evita exponer la entidad de dominio directamente a la capa de presentación.
            return new PersonaDto
            {
                Id = persona.Id,
                Nombre = persona.Nombre,
                ApPaterno = persona.ApPaterno,
                ApMaterno = persona.ApMaterno,
                Mail = persona.Mail,
                CI = persona.CI,
                Nit = persona.Nit,
                Direccion = persona.Direccion,
                Telefono = persona.Telefono,
                Celular = persona.Celular,
                FechaCreacion = persona.FechaCreacion
            };
        }
    }
}