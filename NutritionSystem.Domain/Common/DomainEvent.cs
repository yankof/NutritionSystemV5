namespace NutritionSystem.Domain.Common;

// Clase base abstracta para todos los eventos de dominio
// Ahora implementa INotification de MediatR
//public abstract class DomainEventClas : INotification // <-- Modifica esta línea
//{
//    public Guid Id { get; set; }
//    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;

//    protected DomainEventClas(Guid id, DateTime dateOccurred)
//    {
//        Id = Guid.NewGuid();
//        DateOccurred = dateOccurred;
//    }
//    public DomainEventClas()
//    {
//        Id = Guid.NewGuid();
//        DateOccurred = DateTime.Now;
//    }
    
//}

public abstract record DomainEvent : INotification
{
    public Guid Id { get; set; }
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;

    public DomainEvent()
    {
        Id = Guid.NewGuid();
        DateOccurred = DateTime.Now;
    }
}