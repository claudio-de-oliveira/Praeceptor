using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Commands
{
    public class UpdateSubSubSectionCommandHandler
        : IRequestHandler<UpdateSubSubSectionCommand, ErrorOr<SubSubSectionResult>>
    {
        private readonly ISubSubSectionRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateSubSubSectionCommandHandler(ISubSubSectionRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<SubSubSectionResult>> Handle(UpdateSubSubSectionCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSubSection.Canceled;

            var entity = await _repository.GetSubSubSectionById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.SubSubSection.NotFound;

            var updated = new Domain.Entities.SubSubSection(request.Id)
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

            await _repository.UpdateSubSubSection(updated);

            return new SubSubSectionResult(updated);
        }
    }
}

