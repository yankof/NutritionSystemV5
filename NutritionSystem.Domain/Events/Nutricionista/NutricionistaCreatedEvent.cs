namespace NutritionSystem.Domain.Events.Nutricionista
{
    // Evento que se dispara cuando un nuevo nutricionista es creado
    public record NutricionistaCreatedEvent : DomainEvent
    {
        public Guid NutricionistaId { get; }
        public string Titulo { get; }

        public NutricionistaCreatedEvent(Guid nutricionistaId, string titulo)
        {
            NutricionistaId = nutricionistaId;
            Titulo = titulo;
        }
    }
}
