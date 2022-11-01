using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.Section.Commands
{
    public class CreateSectionCommandHandler
        : IRequestHandler<CreateSectionCommand, ErrorOr<SectionResult>>
    {
        private readonly ISectionRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateSectionCommandHandler(ISectionRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<SectionResult>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.Section.Create(
                request.Title,
                request.Text,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                request.CreatedBy);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Section.Canceled;

            var created = await _repository.CreateSection(entity);
            if (created is null)
                return Domain.Errors.Error.Section.DataBaseError;

            return new SectionResult(created);
        }
    }
}

