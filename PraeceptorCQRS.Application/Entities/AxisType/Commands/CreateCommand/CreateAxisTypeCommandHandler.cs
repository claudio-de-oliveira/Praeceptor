using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.AxisType.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;
using PraeceptorCQRS.Domain.DomainEvents;

namespace PraeceptorCQRS.Application.Entities.AxisType.Commands.CreateCommand
{
    public class CreateAxisTypeCommandHandler
        : IRequestHandler<CreateAxisTypeCommand, ErrorOr<AxisTypeResult>>
    {
        private readonly IAxisTypeRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateAxisTypeCommandHandler(IAxisTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<AxisTypeResult>> Handle(CreateAxisTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.AxisType.Create(
                request.Code,
                request.Code3,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                string.Empty);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.AxisType.Canceled;

            var created = await _repository.CreateAxisType(entity);
            if (created is null)
                return Domain.Errors.Error.AxisType.DataBaseError;

            return new AxisTypeResult(created);
        }
    }
}