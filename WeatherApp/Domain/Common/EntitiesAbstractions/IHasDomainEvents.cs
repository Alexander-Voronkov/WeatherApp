namespace Domain.Common.EntitiesAbstractions;

public interface IHasDomainEvents
{
    public IReadOnlyCollection<DomainEvent> DomainEvents { get; }

    public void ClearDomainEvents();
}