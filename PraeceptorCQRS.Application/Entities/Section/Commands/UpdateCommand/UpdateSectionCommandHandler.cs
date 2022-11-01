using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Section.Commands
{
    public class UpdateSectionCommandHandler
        : IRequestHandler<UpdateSectionCommand, ErrorOr<SectionResult>>
    {
        private readonly ISectionRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateSectionCommandHandler(ISectionRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<SectionResult>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetSectionById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Section.NotFound;

            var updated = new Domain.Entities.Section(request.Id)
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
                return Domain.Errors.Error.Section.Canceled;

            await _repository.UpdateSection(updated);

            return new SectionResult(updated);
        }
    }
}

