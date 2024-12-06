using Domain.Common.EntitiesAbstractions;
using Domain.UserAggregate;

namespace Wilby.Domain.Events;
public record UserCreatedDomainEvent(UserId Id) : DomainEvent, IDomainNotification;