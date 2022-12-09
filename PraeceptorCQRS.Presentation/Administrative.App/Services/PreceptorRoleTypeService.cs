using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.PreceptorRoleType;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services;

public class PreceptorRoleService : HttpAbstractService, IPreceptorRoleService
{
    private readonly ConfigurationManager _configuration;

    public PreceptorRoleService(ConfigurationManager configuration)
        : base(
            $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/preceptorroletype/",
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

    public async Task<HttpResponseMessage> CreatePreceptorRoleType(CreatePreceptorRoleTypeRequest request)
        => await base.Create<CreatePreceptorRoleTypeRequest>(request, "create");
    public async Task<HttpResponseMessage> DeletePreceptorRoleType(Guid id)
        => await base.Delete("delete", id);

    public async Task<PreceptorRoleTypeModel?> GetPreceptorRoleTypeById(Guid id)
        => await base.GetOne<PreceptorRoleTypeModel>("get", "id", id);
    public async Task<PreceptorRoleTypeModel?> GetPreceptorRoleTypeByCode(Guid instituteId, string code)
        => await base.GetOne<PreceptorRoleTypeModel>("get", "code", instituteId, code);

    public async Task<int> GetPreceptorRoleTypeCount(Guid instituteId)
        => await base.Count("get", "count", instituteId);
    public async Task<HttpResponseMessage> PostPage(GetPreceptorRoleTypePageRequest request)
        => await base.Create<GetPreceptorRoleTypePageRequest>(request, "get", "page");
    public async Task<HttpResponseMessage> UpdatePreceptorRoleType(UpdatePreceptorRoleTypeRequest request)
        => await base.Update(request, "update");
}
