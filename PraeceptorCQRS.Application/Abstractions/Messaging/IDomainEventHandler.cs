using MediatR;
using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
