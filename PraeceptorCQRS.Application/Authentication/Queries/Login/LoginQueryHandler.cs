using ErrorOr;
using MediatR;
using PraeceptorCQRS.Application.Authentication.Common;
using PraeceptorCQRS.Application.Persistence;

namespace PraeceptorCQRS.Application.Authentication.Queries
{
    public class LoginQueryHandler
        : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        // private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IApplicationUserRepository _userRepository;

        public LoginQueryHandler(/*IJwtTokenGenerator jwtTokenGenerator,*/ IApplicationUserRepository userRepository)
        {
            // _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // 1. Validate the user exists
            var user = await _userRepository.GetApplicationUserByUserName(query.UserName);
            if (user is null)
                return Domain.Errors.Error.Authentication.InvalidCredentials;

            // 2. Validate the password is correct
            if (user.PasswordHash != query.Password)
                return Domain.Errors.Error.Authentication.InvalidCredentials;

            // 3. Create JWT token
            // var token = _jwtTokenGenerator.StringGenerator(user);
            var token = "";

            return new AuthenticationResult(
                user,
                token);
        }
    }
}

