using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Institute;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services;

public class InstituteService : HttpAbstractService, IInstituteService
{
    private readonly ConfigurationManager _configuration;

    public InstituteService(ConfigurationManager configuration)
        : base(
            $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/institute/",
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

    public async Task<HttpResponseMessage> CreateInstitute(CreateInstituteRequest request)
        => await base.Create<CreateInstituteRequest>(request, "create");
    public async Task<HttpResponseMessage> DeleteInstitute(Guid id)
        => await base.Delete("delete", id);

    public async Task<InstituteModel?> GetInstituteById(Guid id)
    {
        var item = await base.GetOne<InstituteModel>("get", "id", id);
        if (item is null)
            return null;
        return item;
    }
    public async Task<InstituteModel?> GetInstituteByCode(string code)
    {
        var item = await base.GetOne<InstituteModel>("get", "code", code);
        if (item is null)
            return null;
        return item;
    }

    public async Task<int> GetInstituteCount(Guid holdingId)
        => await base.Count("get", "count", holdingId);
    public async Task<HttpResponseMessage> PostPage(GetInstitutePageRequest request)
        => await base.Create<GetInstitutePageRequest>(request, "get", "page");
    public async Task<HttpResponseMessage> UpdateInstitute(UpdateInstituteRequest request)
        => await base.Update(request, "update");

}
