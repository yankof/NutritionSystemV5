using NutritionSystem.Domain.Entities;

namespace NutritionSystem.Application.Features.Consulta.Commands
{
    public class CreateConsultaCommand : IRequest<Guid>
    {
        public Guid PacienteId { get; set; }
        public Guid NutricionistaId { get; set; }
        public DateOnly FechaConsulta { get; set; }
        //public string Motivo { get; set; }
        public string Notas { get; set; }
    }

    public class CreateConsultaCommandHandler : IRequestHandler<CreateConsultaCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateConsultaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateConsultaCommand request, CancellationToken cancellationToken)
        {
            // Validar que el Paciente y el Nutricionista existan
            var paciente = await _unitOfWork.Pacientes.GetByIdAsync(request.PacienteId);
            if (paciente == null)
            {
                throw new ArgumentException($"El Paciente con ID {request.PacienteId} no existe.");
            }
            var nutricionista = await _unitOfWork.Nutricionistas.GetByIdAsync(request.NutricionistaId);
            if (nutricionista == null)
            {
                throw new ArgumentException($"El Nutricionista con ID {request.NutricionistaId} no existe.");
            }

            var consulta = new Domain.Entities.Consulta(
                Guid.NewGuid(), // Generar un nuevo ID para la consulta
                request.Notas,
                request.PacienteId,                
                request.NutricionistaId
                //request.FechaConsulta
                //request.Motivo,
                
            );
            

            await _unitOfWork.Consultas.AddAsync(consulta);
            await _unitOfWork.CompleteAsync(); // Esto disparará el evento de dominio

            return consulta.Id;
        }
    }
}