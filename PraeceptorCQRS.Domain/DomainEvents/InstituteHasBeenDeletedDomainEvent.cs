namespace PraeceptorCQRS.Domain.DomainEvents;

public sealed record InstituteHasBeenDeletedDomainEvent(
    Guid Id,
    Guid InstituteId
    ) : DomainEvent(Id);
