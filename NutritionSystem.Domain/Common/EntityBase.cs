namespace NutritionSystem.Domain.Common
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; protected set; }

        // Lista de eventos de dominio generados por esta entidad
        private List<DomainEvent> _domainEvents = new List<DomainEvent>();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}