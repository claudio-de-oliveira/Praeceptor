using PraeceptorCQRS.Application.Abstractions.Messaging;
using PraeceptorCQRS.Domain.DomainEvents;

namespace PraeceptorCQRS.Application.Entities.Institute.Events;

internal sealed class InstituteHasBeenDeletedDomainEventHandler
    : IDomainEventHandler<InstituteHasBeenDeletedDomainEvent>
{
    public async Task Handle(InstituteHasBeenDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++");
        Console.WriteLine($"Foi deletado o instituto com código {notification.Id}");
        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++");
        await Task.CompletedTask;
    }
}
