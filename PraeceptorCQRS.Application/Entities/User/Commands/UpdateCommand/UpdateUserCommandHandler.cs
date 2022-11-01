using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Entities.User.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Entities.User.Commands.UpdateCommand
{
    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, ErrorOr<UserResult>>
    {
        private readonly IApplicationUserRepository _repository;

        public UpdateUserCommandHandler(IApplicationUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<UserResult>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetApplicationUserById(request.Id);
            if (entity is null)
                return Domain.Errors.Error.User.NotFound;

            var updated = new Domain.Entities.ApplicationUser
            {
                Id = request.Id.ToString(),
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpper(),
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                // Não muda senha por aqui
                PasswordHash = entity.PasswordHash,
                PhoneNumber = request.PhoneNumber,
                IsEnabled = request.IsEnabled,
                Gender = request.Gender,
                HoldingId = request.HoldingId,
                InstituteId = request.InstituteId,
                CourseId = request.CourseId
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.User.Canceled;

            await _repository.UpdateApplicationUser(updated);

            return new UserResult(updated);
        }
    }
}

