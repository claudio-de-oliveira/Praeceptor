using MediatR;

namespace PraeceptorCQRS.Domain.Base;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
