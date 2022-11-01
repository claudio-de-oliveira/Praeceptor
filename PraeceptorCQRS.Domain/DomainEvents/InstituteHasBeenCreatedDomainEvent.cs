namespace PraeceptorCQRS.Domain.DomainEvents;

public sealed record InstituteHasBeenCreatedDomainEvent(
    Guid Id,
    Guid InstituteId,
    Guid HoldingId
    ) : DomainEvent(Id);
