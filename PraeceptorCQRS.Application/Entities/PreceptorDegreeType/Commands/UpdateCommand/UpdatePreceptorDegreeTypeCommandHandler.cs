using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.PreceptorDegreeType.Commands
{
    public class UpdatePreceptorDegreeTypeCommandHandler
        : IRequestHandler<UpdatePreceptorDegreeTypeCommand, ErrorOr<PreceptorDegreeTypeResult>>
    {
        private readonly IPreceptorDegreeTypeRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdatePreceptorDegreeTypeCommandHandler(IPreceptorDegreeTypeRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<PreceptorDegreeTypeResult>> Handle(UpdatePreceptorDegreeTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetPreceptorDegreeTypeById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.PreceptorDegreeType.NotFound;

            var updated = new Domain.Entities.PreceptorDegreeType(request.Id)
            {
                Code = entity.Code,
                // don't change institute!
                InstituteId = entity.InstituteId,
                // ...
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = string.Empty
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.PreceptorDegreeType.Canceled;

            await _repository.UpdatePreceptorDegreeType(updated);

            return new PreceptorDegreeTypeResult(updated);
        }
    }
}

