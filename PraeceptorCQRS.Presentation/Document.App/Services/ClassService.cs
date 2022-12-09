using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Class;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services;

public class ClassService : HttpAbstractService, IClassService
{
    private readonly ConfigurationManager _configuration;

    public ClassService(ConfigurationManager configuration)
        : base(
            $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/class/",
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

    public async Task<int> GetClassCount(Guid instituteId)
        => await base.Count("get", "count", instituteId);

    public async Task<HttpResponseMessage> PostPage(GetClassPageRequest request)
        => await base.Create<GetClassPageRequest>(request, "get", "page");

    public async Task<ClassModel?> GetClassByCode(string code)
    {
        var item = await base.GetOne<ClassModel>("get", "code", code);
        if (item is null)
            return null;
        return item;
    }
}