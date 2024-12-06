using MediatR;
using Domain.Common.EntitiesAbstractions;

namespace Application.Common.Interfaces.Events;
internal interface IDomainEventHandler<in TNotification> : INotificationHandler<TNotification>
    where TNotification : DomainEvent;