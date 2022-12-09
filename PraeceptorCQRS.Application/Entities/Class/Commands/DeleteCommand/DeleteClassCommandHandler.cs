using ErrorOr;

using MediatR;

using PraeceptorCQRS.Application.Entities.Class.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.Class.Commands
{
    public class DeleteClassCommandHandler
        : IRequestHandler<DeleteClassCommand, ErrorOr<ClassResult>>
    {
        private readonly IClassRepository _repository;

        public DeleteClassCommandHandler(IClassRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ClassResult>> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.Class.Canceled;

            var entity = await _repository.GetClassById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.Class.NotFound;

            await _repository.DeleteClass(request.Id);

            return new ClassResult(entity);
        }
    }
}