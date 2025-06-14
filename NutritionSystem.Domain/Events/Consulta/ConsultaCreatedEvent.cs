namespace NutritionSystem.Domain.Events.Consulta
{
    public record ConsultaCreatedEvent : DomainEvent
    {
        public Guid ConsultaId { get; }
        public Guid PacienteId { get; }
        public Guid NutricionistaId { get; }
        public DateOnly FechaConsulta { get; }

        public ConsultaCreatedEvent(Guid consultaId, Guid pacienteId, Guid nutricionistaId, DateOnly fechaConsulta)
        {
            ConsultaId = consultaId;
            PacienteId = pacienteId;
            NutricionistaId = nutricionistaId;
            FechaConsulta = fechaConsulta;
        }
    }
}