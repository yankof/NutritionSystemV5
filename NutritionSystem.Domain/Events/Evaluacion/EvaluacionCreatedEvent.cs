namespace NutritionSystem.Domain.Events.Evaluacion
{
    public record EvaluacionCreatedEvent : DomainEvent
    {
        public Guid EvaluacionId { get; }
        public Guid ConsultaId { get; }
        public TipoEvaluacion TipoEvaluacion { get; }

        public EvaluacionCreatedEvent(Guid evaluacionId, Guid consultaId, TipoEvaluacion tipoEvaluacion)
        {
            EvaluacionId = evaluacionId;
            ConsultaId = consultaId;
            TipoEvaluacion = tipoEvaluacion;
        }
    }
}