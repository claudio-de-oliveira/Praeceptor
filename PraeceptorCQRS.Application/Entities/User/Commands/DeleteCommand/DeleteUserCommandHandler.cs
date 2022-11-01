using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.User.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.User.Commands.DeleteCommand
{
    public class DeleteUserCommandHandler
        : IRequestHandler<DeleteUserCommand, ErrorOr<UserResult>>
    {
        private readonly IApplicationUserRepository _repository;

        public DeleteUserCommandHandler(IApplicationUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<UserResult>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetApplicationUserById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.User.NotFound;

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.User.Canceled;

            await _repository.DeleteApplicationUser(request.Id);

            return new UserResult(entity);
        }
    }
}
