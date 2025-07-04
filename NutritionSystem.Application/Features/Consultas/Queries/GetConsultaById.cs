﻿namespace NutritionSystem.Application.Features.Consulta.Queries
{
    public class GetConsultaByIdQuery : IRequest<ConsultaDto>
    {
        public Guid Id { get; set; }
    }

    public class GetConsultaByIdQueryHandler : IRequestHandler<GetConsultaByIdQuery, ConsultaDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetConsultaByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ConsultaDto> Handle(GetConsultaByIdQuery request, CancellationToken cancellationToken)
        {
            var consulta = await _unitOfWork.Consultas.GetByIdAsync(request.Id);

            if (consulta == null)
            {
                return null;
            }

            // Mapea la entidad a un DTO. Puedes usar AutoMapper aquí.
            return new ConsultaDto
            {
                Id = consulta.Id,
                PacienteId = consulta.IdPacient,
                NutricionistaId = consulta.IdNutricionista,
                FechaConsulta = consulta.FechaCreacion,
                Motivo = consulta.Descripcion,
                Notas = consulta.Descripcion,
                Estatus = consulta.Estatus.ToString() // Convierte el enum a string para el DTO
                // Puedes incluir otros campos si es necesario
            };
        }
    }

}