using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.PreceptorRegimeType.Commands
{
    public class UpdatePreceptorRegimeTypeCommandHandler
        : IRequestHandler<UpdatePreceptorRegimeTypeCommand, ErrorOr<PreceptorRegimeTypeResult>>
    {
        private readonly IPreceptorRegimeTypeRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdatePreceptorRegimeTypeCommandHandler(IPreceptorRegimeTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<PreceptorRegimeTypeResult>> Handle(UpdatePreceptorRegimeTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetPreceptorRegimeTypeById(request.Id);

            if (entity is null)
                return Domain.Errors.Error.PreceptorRegimeType.NotFound;

            var updated = new Domain.Entities.PreceptorRegimeType(request.Id)
            {
                Code = request.Code,
                Code3 = request.Code3,
                // don't change institute!
                InstituteId = entity.InstituteId,
                // ...
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = string.Empty
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorRegimeType.Canceled;

            await _repository.UpdatePreceptorRegimeType(updated);

            return new PreceptorRegimeTypeResult(updated);
        }
    }
}

