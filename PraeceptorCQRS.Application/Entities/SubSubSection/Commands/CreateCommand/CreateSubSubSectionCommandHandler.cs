using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Commands
{
    public class CreateSubSubSectionCommandHandler
        : IRequestHandler<CreateSubSubSectionCommand, ErrorOr<SubSubSectionResult>>
    {
        private readonly ISubSubSectionRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateSubSubSectionCommandHandler(ISubSubSectionRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<SubSubSectionResult>> Handle(CreateSubSubSectionCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.SubSubSection.Create(
                request.Title,
                request.Text,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                request.CreatedBy);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSubSection.Canceled;

            var created = await _repository.CreateSubSubSection(entity);
            if (created is null)
                return Domain.Errors.Error.SubSubSection.DataBaseError;

            return new SubSubSectionResult(created);
        }
    }
}

