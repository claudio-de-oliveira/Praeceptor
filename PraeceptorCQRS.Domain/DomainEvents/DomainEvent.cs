using PraeceptorCQRS.Domain.Base;

namespace PraeceptorCQRS.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
