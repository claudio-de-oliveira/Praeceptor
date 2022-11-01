using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.ClassType.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.ClassType.Commands
{
    public class DeleteClassTypeCommandHandler
        : IRequestHandler<DeleteClassTypeCommand, ErrorOr<ClassTypeResult>>
    {
        private readonly IClassTypeRepository _repository;

        public DeleteClassTypeCommandHandler(IClassTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<ClassTypeResult>> Handle(DeleteClassTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetClassTypeById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.ClassType.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.ClassType.Canceled;

            await _repository.DeleteClassType(request.Id);

            return new ClassTypeResult(entity);
        }
    }
}

