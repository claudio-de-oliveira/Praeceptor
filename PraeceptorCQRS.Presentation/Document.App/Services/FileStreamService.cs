using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.DocumentTemplate;
using PraeceptorCQRS.Contracts.Entities.SqlFileStream;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services
{
    public class FileStreamService : HttpAbstractService, IFileStreamService
    {
        private readonly ConfigurationManager _configuration;

        public FileStreamService(ConfigurationManager configuration)
            : base(
                $"{configuration.GetSection("FileStream.API:applicationUrl").Value}/filestream/",
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
                ClientId = _configuration.GetSection("Document.APP:clientId").Value,
                ClientSecret = _configuration.GetSection("Document.APP:clientSecret").Value,
            });

            return response.AccessToken;
        }

        public async Task<bool> ExistFileStream(Guid instituteId, string code)
            => await base.Exist("exist", instituteId, code); 

        public async Task<FileModel?> GetFileStreamById(Guid id)
        {
            var item = await base.GetOne<FileModel>("get", "id", id);
            if (item is null)
                return null;
            return item;
        }
        public async Task<FileModel?> GetFileStreamByCode(Guid instituteId, string code)
        {
            var item = await base.GetOne<FileModel>("get", "code", instituteId, code);
            if (item is null)
                return null;
            return item;
        }
        public async Task<int> GetFileStreamByInstituteCount(Guid instituteId)
            => await base.Count("get", "count", instituteId);
        public async Task<HttpResponseMessage> GetFileStreamPage(GetFilePageRequest request)
            => await base.Create<GetFilePageRequest>(request, "get", "page");
        public async Task<HttpResponseMessage> CreateFileStream(CreateFileRequest request)
            => await base.Create<CreateFileRequest>(request, "create");
        public async Task<HttpResponseMessage> DeleteFileStream(Guid id)
            => await base.Delete("delete", id);
    }
}
