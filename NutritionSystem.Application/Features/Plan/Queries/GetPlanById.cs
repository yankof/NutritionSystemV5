namespace NutritionSystem.Application.Features.Consulta.Queries
{
    public class GetPlanByIdQuery : IRequest<PlanDto>
    {
        public Guid Id { get; set; }
    }

    public class GetPlanByIdQueryHandler : IRequestHandler<GetPlanByIdQuery, PlanDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPlanByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PlanDto> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var plan = await _unitOfWork.Planes.GetByIdAsync(request.Id);

            if (plan == null)
            {
                return null;
            }

            // Mapea la entidad a un DTO. Puedes usar AutoMapper aquí.
            return new PlanDto
            {
                Id = plan.Id,
                ConsultaId = plan.ConsultaId,
                Descripcion = plan.Descripcion,
                TipoPlan = (TipoPlan)int.Parse(plan.TipoPlanClave),
                FechaCreacion = plan.FechaCreacion,
                //TipoStatus = plan.TipoStatus,
                DiasTratamiento = plan.DiasTratamiento, // Convierte el enum a string para el DTO
                IdContrato = string.IsNullOrEmpty(plan.IdContrato.ToString()) ? "" : plan.IdContrato.ToString(),
                // Puedes incluir otros campos si es necesario
            };
        }
    }

    
}