namespace NutritionSystem.Application.Features.Consulta.Events
{
    public class ConsultaCreatedEventHandler : INotificationHandler<ConsultaCreatedEvent>
    {
        private readonly ILogger<ConsultaCreatedEventHandler> _logger;

        public ConsultaCreatedEventHandler(ILogger<ConsultaCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ConsultaCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Domain Event: Consulta creada: {ConsultaId} para Paciente {PacienteId} con Nutricionista {NutricionistaId} en la fecha {FechaConsulta}. " +
                "Fecha del evento: {Fecha}",
                notification.ConsultaId,
                notification.PacienteId,
                notification.NutricionistaId,
                notification.FechaConsulta,
                notification.DateOccurred);

            // Ejemplo: Podrías enviar un correo de confirmación de la consulta al paciente y nutricionista
            // o crear una entrada en un calendario externo.

            return Task.CompletedTask;
        }
    }
}