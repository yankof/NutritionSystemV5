using NutritionSystem.Application.Features.Consulta.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionSystem.Application.Features.Plan.Queries;
public class GetAllPlanQuery : IRequest<IEnumerable<PlanDto>> { }

// Manejador de la Query
public class GetAllPlanQueryHandler : IRequestHandler<GetAllPlanQuery, IEnumerable<PlanDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPlanQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PlanDto>> Handle(GetAllPlanQuery request, CancellationToken cancellationToken)
    {
        var planes = await _unitOfWork.Planes.GetAllAsync();

        // Mapear la colección de entidades a una colección de DTOs
        return planes.Select(plan => new PlanDto
        {
            //TipoPlan x = (TipoPlan)((Int32)plan.TipoPlanClave),
            Id = plan.Id,
            ConsultaId = plan.ConsultaId,
            Descripcion = plan.Descripcion,
            TipoPlan = (TipoPlan)int.Parse(plan.TipoPlanClave),
            FechaCreacion = plan.FechaCreacion,
            //TipoStatus = plan.TipoStatus,
            DiasTratamiento = plan.DiasTratamiento, // Convierte el enum a string para el DTO
            IdContrato = string.IsNullOrEmpty(plan.IdContrato.ToString()) ? "" : plan.IdContrato.ToString(),
        }).ToList();
    }
}

