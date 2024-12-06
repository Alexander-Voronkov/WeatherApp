using Domain.Common.DomainExceptions;

namespace Domain.Common.EntitiesAbstractions;
public abstract class BaseEntity<TEntityId, TEntityIdType> : IHasDomainEvents
    where TEntityId : BaseEntityId<TEntityIdType>
{
    public TEntityId Id { get; protected set; }

    private List<DomainEvent> _domainEvents;

    /// <summary>
    /// Domain events occurred.
    /// </summary>
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    /// <summary>
    /// Add domain event.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents ??= [];

        _domainEvents.Add(domainEvent);
    }

    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}