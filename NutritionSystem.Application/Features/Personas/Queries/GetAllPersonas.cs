using MediatR;
using NutritionSystem.Application.DTOs;
using NutritionSystem.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NutritionSystem.Application.Features.Personas.Queries
{
    // Query para obtener todas las personas
    public class GetAllPersonasQuery : IRequest<IEnumerable<PersonaDto>> { }

    // Manejador de la Query
    public class GetAllPersonasQueryHandler : IRequestHandler<GetAllPersonasQuery, IEnumerable<PersonaDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPersonasQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PersonaDto>> Handle(GetAllPersonasQuery request, CancellationToken cancellationToken)
        {
            var personas = await _unitOfWork.Personas.GetAllAsync();

            // Mapear la colección de entidades a una colección de DTOs
            return personas.Select(persona => new PersonaDto
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
            }).ToList();
        }
    }
}