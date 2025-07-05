namespace NutritionSystem.Domain.Common;

public abstract record DomainEvent : INotification
{
    public Guid Id { get; set; }
    public DateTime DateOccurred { get;  set; } = DateTime.UtcNow;

    public DomainEvent()
    {
        Id = Guid.NewGuid();
        DateOccurred = DateTime.Now;
    }
}