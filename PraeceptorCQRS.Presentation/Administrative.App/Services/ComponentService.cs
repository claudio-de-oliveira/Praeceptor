using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Component;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services
{
    public class ComponentService : HttpAbstractService, IComponentService
    {
        private readonly ConfigurationManager _configuration;

        public ComponentService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/component/",
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
                ClientId = _configuration.GetSection("Administrative.APP:clientId").Value,
                ClientSecret = _configuration.GetSection("Administrative.APP:clientSecret").Value,
            });

            return response.AccessToken;
        }

        public async Task<HttpResponseMessage> CreateComponent(CreateComponentRequest request)
            => await base.Create<CreateComponentRequest>(request, "create");
        public async Task<HttpResponseMessage> UpdateComponent(UpdateComponentRequest request)
            => await base.Update(request, "update");
        public async Task<ComponentModel?> GetComponentByCourseAndCurriculumAndClass(Guid courseId, int curriculum, Guid classId)
            => await base.GetOne<ComponentModel>("get", "class", courseId, curriculum, classId);
        public async Task<IEnumerable<ComponentModel>?> GetComponentListByCourseAndCurriculum(Guid courseId, int curriculum)
            => await base.GetMany<ComponentModel>("get", "curriculum", courseId, curriculum);
        public async Task<IEnumerable<ComponentModel>?> GetComponentListByCourseAndCurriculumAndSeason(Guid courseId, int curriculum, int season)
            => await base.GetMany<ComponentModel>("get", "season", courseId, curriculum, season);
        public async Task<List<CurriculumModel>?> GetCurriculumsByCourseId(Guid courseId)
            => await base.GetMany<CurriculumModel>("list", "curriculum", courseId);
        public async Task<HttpResponseMessage> DeleteComponent(Guid courseId, int curriculum, Guid classId)
            => await base.Delete("delete", courseId, curriculum, classId);
    }
}
