using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.GroupValue.Commands.DeleteCommand
{
    public class DeleteGroupValuesFromGroupCommandHandler
        : IRequestHandler<DeleteGroupValuesFromGroupCommand, ErrorOr<bool>>
    {
        private readonly IGroupValueRepository _repository;

        public DeleteGroupValuesFromGroupCommandHandler(IGroupValueRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteGroupValuesFromGroupCommand request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.GroupValue.Canceled;

            return await _repository.DeleteGroupValuesFromGroup(request.GroupId);
        }
    }
}
