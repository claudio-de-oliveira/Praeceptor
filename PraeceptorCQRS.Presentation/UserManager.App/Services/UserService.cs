using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.User;
using PraeceptorCQRS.Utilities;

using UserManager.App.Interfaces;
using UserManager.App.Models;

using static IdentityModel.OidcConstants;

namespace UserManager.App.Services
{
    public class UserService : HttpAbstractService, IUserService
    {
        private readonly ConfigurationManager _configuration;

        public UserService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("UserManager.API:applicationUrl").Value}/user/",
                configuration.GetSection("IdentityServer:Authority").Value,
                new HttpClient(new HttpClientHandler
                {
                    // Bypass the SSH certificate
                    ServerCertificateCustomValidationCallback =
                        (sender, cert, chain, sslPolicyErrors) => { return true; }
                })
            )
        {
            _configuration = configuration;
        }

        protected override async Task<string> GetAccessToken()
        {
            var response = await _httpClient.RequestTokenAsync(new IdentityModel.Client.TokenRequest
            {
                Address = TokenEndpoint,
                GrantType = GrantTypes.ClientCredentials,
                ClientId = _configuration.GetSection("UserManager.APP:clientId").Value,
                ClientSecret = _configuration.GetSection("UserManager.APP:clientSecret").Value,
            });

            return response.AccessToken;
        }

        public async Task<HttpResponseMessage> CreateUser(CreateUserRequest request)
            => await base.Create<CreateUserRequest>(request, "create");

        public async Task<HttpResponseMessage> DeleteUser(Guid id)
            => await base.Delete("delete", id);

        public async Task<UserModel?> GetUserById(Guid id)
        {
            var item = await base.GetOne<UserModel>("get", "id", id);
            if (item is null)
                return null;
            return item;
        }

        public async Task<int> GetUserCount(Guid instituteId)
            => await base.Count("get", "count", instituteId);
        public async Task<HttpResponseMessage> PostPage(GetUserPageRequest request)
            => await base.Create<GetUserPageRequest>(request, "get", "page");
        public async Task<HttpResponseMessage> UpdateUser(UpdateUserRequest request)
            => await base.Update(request, "update");
    }
}
