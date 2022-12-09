using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.ClassType;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services;

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
            ClientId = _configuration.GetSection("Administrative.APP:clientId").Value,
            ClientSecret = _configuration.GetSection("Administrative.APP:clientSecret").Value,
        });

        return response.AccessToken;
    }

    public async Task<HttpResponseMessage> CreateClassType(CreateClassTypeRequest request)
        => await base.Create<CreateClassTypeRequest>(request, "create");

    public async Task<HttpResponseMessage> DeleteClassType(Guid id)
        => await base.Delete("delete", id);

    public async Task<ClassTypeModel?> GetClassTypeById(Guid id)
    {
        var item = await base.GetOne<ClassTypeModel>("get", "id", id);
        if (item is null)
            return null;
        return item;
    }

    public async Task<ClassTypeModel?> GetClassTypeByCode(Guid instituteId, string code)
    {
        var item = await base.GetOne<ClassTypeModel>("get", "code", instituteId, code);
        if (item is null)
            return null;
        return item;
    }

    public async Task<int> GetClassTypeCount(Guid instituteId)
        => await base.Count("get", "count", instituteId);

    public async Task<HttpResponseMessage> PostPage(GetClassTypePageRequest request)
        => await base.Create<GetClassTypePageRequest>(request, "get", "page");

    public async Task<HttpResponseMessage> UpdateClassType(UpdateClassTypeRequest request)
        => await base.Update(request, "update");
}