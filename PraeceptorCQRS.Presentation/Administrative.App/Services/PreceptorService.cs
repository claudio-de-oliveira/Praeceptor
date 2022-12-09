using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Preceptor;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services;

public class PreceptorService : HttpAbstractService, IPreceptorService
{
    private readonly ConfigurationManager _configuration;

    public PreceptorService(ConfigurationManager configuration)
        : base(
            $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/preceptor/",
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

    public async Task<HttpResponseMessage> CreatePreceptor(CreatePreceptorRequest request)
        => await base.Create<CreatePreceptorRequest>(request, "create");

    public async Task<PreceptorModel?> GetPreceptorById(Guid id)
        => await base.GetOne<PreceptorModel>("get", "id", id);

    public async Task<PreceptorModel?> GetPreceptorByCode(string code)
        => await base.GetOne<PreceptorModel>("get", "code", code);

    public async Task<int> GetPreceptorCount(Guid instituteId)
        => await base.Count("get", "count", instituteId);

    public async Task<HttpResponseMessage> PostPage(GetPreceptorPageRequest request)
        => await base.Create<GetPreceptorPageRequest>(request, "get", "page");

    public async Task<HttpResponseMessage> UpdatePreceptor(UpdatePreceptorRequest request)
        => await base.Update(request, "update");

    public async Task<HttpResponseMessage> DeletePreceptor(Guid id)
        => await base.Delete("delete", id);
}