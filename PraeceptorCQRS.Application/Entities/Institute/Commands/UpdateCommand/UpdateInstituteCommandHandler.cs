using ErrorOr;
using MediatR;

using PraeceptorCQRS.Application.Entities.Institute.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Institute.Commands
{
    public class UpdateInstituteCommandHandler
        : IRequestHandler<UpdateInstituteCommand, ErrorOr<InstituteResult>>
    {
        private readonly IInstituteRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateInstituteCommandHandler(IInstituteRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<InstituteResult>> Handle(UpdateInstituteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository
                .GetInstituteById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Institute.NotFound;

            entity.Update(
                request.Name,
                request.Address,
                _dateTimeProvider.UtcNow,
                string.Empty
                );

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Institute.Canceled;

            await _repository.UpdateInstitute(entity);

            return new InstituteResult(entity);
        }
    }
}

