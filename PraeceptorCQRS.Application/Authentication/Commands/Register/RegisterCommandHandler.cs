using ErrorOr;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using PraeceptorCQRS.Application.Authentication.Common;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Domain.Entities;

namespace PraeceptorCQRS.Application.Authentication.Commands
{
    public class RegisterCommandHandler
        : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IApplicationUserRepository _repository;

        public RegisterCommandHandler(/*IJwtTokenGenerator jwtTokenGenerator,*/ IServiceScopeFactory serviceScopeFactory, IApplicationUserRepository userRepository)
        {
            this._repository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            // 1. Validate the user doesn't exists
            if (await _repository.Exists(command.UserName))
                return Domain.Errors.Error.User.DuplicateEmail;

            // 2. Create user (generate unique ID) & Persist to DB
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = command.UserName,
                Email = command.Email,
                Gender = command.Gender,
                PhoneNumber = command.PhoneNumber,
                PasswordHash = command.PasswordHash,
                HoldingId = command.HoldingId,
                InstituteId = command.InstituteId,
                CourseId = command.CourseId,

                NormalizedUserName = command.UserName.ToUpper(),
                NormalizedEmail = command.Email.ToUpper(),
                PhoneNumberConfirmed = false,
                EmailConfirmed = false
            };

            if (cancellationToken.IsCancellationRequested)
                return Domain.Errors.Error.User.Canceled;

            var userCreated = await _repository.CreateApplicationUser(user);
            if (userCreated is null)
                return Domain.Errors.Error.User.DataBaseError;

            // 3. Create JWT token

            // var token = _jwtTokenGenerator.StringGenerator(user);
            var token = "";

            return new AuthenticationResult(
                userCreated,
                token);
        }
    }
}

