namespace NutritionSystem.Application.Features.Diagnostico.Commands
{
    public class CreateDiagnosticoCommand : IRequest<Guid>
    {
        public Guid ConsultaId { get; set; }
        public string Descripcion { get; set; }
        public TipoDiagnostico TipoDiagnostico { get; set; }
        public TipoStatus TipoStatus { get; set; }
    }

    public class CreateDiagnosticoCommandHandler : IRequestHandler<CreateDiagnosticoCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDiagnosticoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateDiagnosticoCommand request, CancellationToken cancellationToken)
        {
            // Opcional: Validar que la ConsultaId exista
            var consulta = await _unitOfWork.Consultas.GetByIdAsync(request.ConsultaId);
            if (consulta == null)
            {
                throw new ArgumentException($"La Consulta con ID {request.ConsultaId} no existe.");
            }

            var diagnostico = new Domain.Entities.Diagnostico(
                Guid.NewGuid(), // Generar un nuevo ID
                request.ConsultaId,
                request.Descripcion,
                request.TipoDiagnostico,
                request.TipoStatus
            );

            await _unitOfWork.Diagnosticos.AddAsync(diagnostico);
            await _unitOfWork.CompleteAsync(); // Esto disparará el evento de dominio

            return diagnostico.Id;
        }
    }
}