using Document.App.Interfaces;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.ClassType;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services;

public class ClassTypeService : HttpAbstractService, IClassTypeService
{
    private readonly ConfigurationManager _configuration;

    public ClassTypeService(ConfigurationManager configuration)
        : base(
            $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/classtype/",
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

    public async Task<int> GetClassTypeCount(Guid instituteId)
        => await base.Count("get", "count", instituteId);

    public async Task<HttpResponseMessage> PostPage(GetClassTypePageRequest request)
        => await base.Create<GetClassTypePageRequest>(request, "get", "page");
}