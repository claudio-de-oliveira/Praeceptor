using Ardalis.GuardClauses;

using PraeceptorCQRS.Application.Abstractions.Messaging;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.DomainEvents;

namespace PraeceptorCQRS.Application.Entities.Institute.Events;

internal sealed class InstituteHasBeenCreatedDomainEventHandler
    : IDomainEventHandler<InstituteHasBeenCreatedDomainEvent>
{
    private readonly IInstituteRepository _instituteRepository;

    public InstituteHasBeenCreatedDomainEventHandler(
        IInstituteRepository instituteRepository
        )
    {
        _instituteRepository = instituteRepository;
    }

    public async Task Handle(InstituteHasBeenCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var institute = await _instituteRepository.GetInstituteById(notification.Id);

        Guard.Against.Null(institute);
        //
        // Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++");
        // Console.WriteLine($"Foi criado o instituto {institute.Name}");
        // Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++");
    }
}