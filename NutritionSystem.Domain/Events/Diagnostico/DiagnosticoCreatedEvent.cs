namespace NutritionSystem.Domain.Events.Diagnostico
{
    public record DiagnosticoCreatedEvent : DomainEvent
    {
        public Guid DiagnosticoId { get; }
        public Guid ConsultaId { get; }
        public TipoDiagnostico TipoDiagnostico { get; }
        public TipoStatus TipoStatus { get; }

        public DiagnosticoCreatedEvent(Guid diagnosticoId, Guid consultaId, 
            TipoDiagnostico tipoDiagnostico, TipoStatus tipoStatus)
        {
            DiagnosticoId = diagnosticoId;
            ConsultaId = consultaId;
            TipoDiagnostico = tipoDiagnostico;
            TipoStatus = tipoStatus;
        }
    }
}