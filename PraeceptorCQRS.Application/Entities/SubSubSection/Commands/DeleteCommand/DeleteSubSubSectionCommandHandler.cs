using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.SubSubSection.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.SubSubSection.Commands
{
    public class DeleteSubSubSectionCommandHandler
        : IRequestHandler<DeleteSubSubSectionCommand, ErrorOr<SubSubSectionResult>>
    {
        private readonly ISubSubSectionRepository _repository;

        public DeleteSubSubSectionCommandHandler(ISubSubSectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<SubSubSectionResult>> Handle(DeleteSubSubSectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetSubSubSectionById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.SubSubSection.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.SubSubSection.Canceled;

            await _repository.DeleteSubSubSection(request.Id);

            return new SubSubSectionResult(entity);
        }
    }
}

