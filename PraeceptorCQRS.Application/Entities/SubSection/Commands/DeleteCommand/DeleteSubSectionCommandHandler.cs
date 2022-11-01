using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSection.Commands
{
    public class DeleteSubSectionCommandHandler
        : IRequestHandler<DeleteSubSectionCommand, ErrorOr<SubSectionResult>>
    {
        private readonly ISubSectionRepository _repository;

        public DeleteSubSectionCommandHandler(ISubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSectionResult>> Handle(DeleteSubSectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetSubSectionById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.SubSection.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSection.Canceled;

            await _repository.DeleteSubSection(request.Id);

            return new SubSectionResult(entity);
        }
    }
}

