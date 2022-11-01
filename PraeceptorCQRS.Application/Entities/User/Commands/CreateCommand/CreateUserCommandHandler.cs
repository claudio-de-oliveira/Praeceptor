using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.User.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.User.Commands.CreateCommand
{
    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, ErrorOr<UserResult>>
    {
        private readonly IApplicationUserRepository _repository;

        public CreateUserCommandHandler(IApplicationUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<UserResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpper(),
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                PasswordHash = request.PasswordHash,
                PhoneNumber = request.PhoneNumber,
                IsEnabled = request.IsEnabled,
                Gender = request.Gender,
                HoldingId = request.HoldingId,
                InstituteId = request.InstituteId,
                CourseId = request.CourseId
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.User.Canceled;

            var created = await _repository.CreateApplicationUser(entity);
            if (created is null)
                return Domain.Errors.Error.User.DataBaseError;

            return new UserResult(created);
        }
    }
}
