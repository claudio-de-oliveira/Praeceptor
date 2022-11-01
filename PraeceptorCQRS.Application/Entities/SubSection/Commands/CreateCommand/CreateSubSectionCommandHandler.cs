using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;

namespace PraeceptorCQRS.Application.Entities.SubSection.Commands
{
    public class CreateSubSectionCommandHandler
        : IRequestHandler<CreateSubSectionCommand, ErrorOr<SubSectionResult>>
    {
        private readonly ISubSectionRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateSubSectionCommandHandler(ISubSectionRepository repository, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<ErrorOr<SubSectionResult>> Handle(CreateSubSectionCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Entities.SubSection.Create(
                request.Title,
                request.Text,
                request.InstituteId,
                _dateTimeProvider.UtcNow,
                request.CreatedBy);

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSection.Canceled;

            var created = await _repository.CreateSubSection(entity);
            if (created is null)
                return Domain.Errors.Error.SubSection.DataBaseError;

            return new SubSectionResult(created);
        }
    }
}

