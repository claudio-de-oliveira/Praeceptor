using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.Section.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Section.Commands
{
    public class DeleteSectionCommandHandler
        : IRequestHandler<DeleteSectionCommand, ErrorOr<SectionResult>>
    {
        private readonly ISectionRepository _repository;

        public DeleteSectionCommandHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SectionResult>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetSectionById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Section.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Section.Canceled;

            await _repository.DeleteSection(request.Id);

            return new SectionResult(entity);
        }
    }
}

