using Ardalis.GuardClauses;

using PraeceptorCQRS.Application.Abstractions.Messaging;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.DomainEvents;

namespace PraeceptorCQRS.Application.Entities.Institute.Events;

internal sealed class InstituteHasBeenUpdatedDomainEventHandler
    : IDomainEventHandler<InstituteHasBeenUpdatedDomainEvent>
{
    private readonly IInstituteRepository _instituteRepository;

    public InstituteHasBeenUpdatedDomainEventHandler(
        IInstituteRepository instituteRepository
        )
    {
        _instituteRepository = instituteRepository;
    }

    public async Task Handle(InstituteHasBeenUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var institute = await _instituteRepository.GetInstituteById(notification.InstituteId);
        Guard.Against.Null(institute);

        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++");
        Console.WriteLine($"Foi modificado o instituto {institute.Name}");
        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++");
    }
}
