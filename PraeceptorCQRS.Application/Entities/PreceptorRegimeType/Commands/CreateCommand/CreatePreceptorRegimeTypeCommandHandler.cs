using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Commands
{
    public class CreatePreceptorRegimeTypeCommandHandler
        : IRequestHandler<CreatePreceptorRegimeTypeCommand, ErrorOr<PreceptorRegimeTypeResult>>
    {
        private readonly IPreceptorRegimeTypeRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreatePreceptorRegimeTypeCommandHandler(IPreceptorRegimeTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<PreceptorRegimeTypeResult>> Handle(CreatePreceptorRegimeTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.PreceptorRegimeType.Create(
                request.Code,
                request.Code3,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                string.Empty
                );

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRegimeType.Canceled;

            var created = await _repository.CreatePreceptorRegimeType(entity);
            if (created is null)
                return Domain.Errors.Error.PreceptorRegimeType.DataBaseError;

            return new PreceptorRegimeTypeResult(created);
        }
    }
}

