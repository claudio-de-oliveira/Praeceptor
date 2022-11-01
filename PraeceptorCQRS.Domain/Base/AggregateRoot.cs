namespace PraeceptorCQRS.Domain.Base;

public abstract class AggregateRoot : BaseAuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(Guid id)
        : base(id)
    { /* NOthing more todo */ }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() 
        => _domainEvents.ToList();
    public void ClearDomainEvents() 
        => _domainEvents.Clear();
    protected void RaiseDomainEvent(IDomainEvent domainEvent) 
        => _domainEvents.Add(domainEvent);
}
