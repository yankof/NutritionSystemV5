namespace NutritionSystem.Application.Features.Evaluacion.Commands
{
    public class CreateEvaluacionCommand : IRequest<Guid>
    {
        public Guid ConsultaId { get; set; }
        public string Descripcion { get; set; }
        public TipoEvaluacion TipoEvaluacion { get; set; }
    }

    public class CreateEvaluacionCommandHandler : IRequestHandler<CreateEvaluacionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEvaluacionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateEvaluacionCommand request, CancellationToken cancellationToken)
        {
            // Opcional: Validar que la ConsultaId exista
            var consulta = await _unitOfWork.Consultas.GetByIdAsync(request.ConsultaId);
            if (consulta == null)
            {
                throw new ArgumentException($"La Consulta con ID {request.ConsultaId} no existe.");
            }

            var evaluacion = new Domain.Entities.Evaluacion(
                Guid.NewGuid(), // Generar un nuevo ID para la evaluación
                request.ConsultaId,
                request.Descripcion,
                request.TipoEvaluacion
            );

            await _unitOfWork.Evaluaciones.AddAsync(evaluacion);
            await _unitOfWork.CompleteAsync(); // Esto disparará el evento de dominio

            return evaluacion.Id;
        }
    }
}