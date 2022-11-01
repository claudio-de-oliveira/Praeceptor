namespace PraeceptorCQRS.Domain.DomainEvents;

public sealed record InstituteHasBeenUpdatedDomainEvent(
    Guid Id,
    Guid InstituteId
    ) : DomainEvent(Id);
