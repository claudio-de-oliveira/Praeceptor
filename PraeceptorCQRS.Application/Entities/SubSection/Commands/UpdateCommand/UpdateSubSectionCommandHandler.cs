using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.SubSection.Commands
{
    public class UpdateSubSectionCommandHandler
        : IRequestHandler<UpdateSubSectionCommand, ErrorOr<SubSectionResult>>
    {
        private readonly ISubSectionRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateSubSectionCommandHandler(ISubSectionRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<SubSectionResult>> Handle(UpdateSubSectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetSubSectionById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.SubSection.NotFound;

            var updated = new Domain.Entities.SubSection(request.Id)
            {
                Title = request.Title,
                Text = request.Text,
                // Don't change InstituteId
                InstituteId = entity.InstituteId,

                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = _dateTimeProvider.UtcNow,
                LastModifiedBy = request.UpdatedBy
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSection.Canceled;

            await _repository.UpdateSubSection(updated);

            return new SubSectionResult(updated);
        }
    }
}

