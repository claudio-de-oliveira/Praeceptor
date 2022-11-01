using ErrorOr;
using MediatR;

using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Institute.Commands
{
    public class CreateInstituteCommandHandler
        : IRequestHandler<CreateInstituteCommand, ErrorOr<InstituteResult>>
    {
        private readonly IInstituteRepository _repository;
        private readonly IHoldingRepository _holdingRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateInstituteCommandHandler(IHoldingRepository holdingRepository, IInstituteRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _holdingRepository = holdingRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<InstituteResult>> Handle(CreateInstituteCommand request, CancellationToken cancellationToken)
        {
            var holding = await _holdingRepository
                .GetHoldingById(request.HoldingId);
            if (holding is null)
                return Domain.Errors.Error.Holding.NotFound;

            var entity = Domain.Entities.Institute.Create(
                request.Acronym,
                request.Name,
                request.Address,
                request.HoldingId,
                _dateTimeProvider.UtcNow,
                string.Empty);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Institute.Canceled;

            var created = await _repository.CreateInstitute(entity);
            if (created is null)
                return Domain.Errors.Error.Institute.DataBaseError;

            return new InstituteResult(created);
        }
    }
}

