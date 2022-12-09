using Document.App.Interfaces;
using Document.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Pea;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Document.App.Services;

public class PlannerService : HttpAbstractService, IPlannerService
{
    private readonly ConfigurationManager _configuration;

    public PlannerService(ConfigurationManager configuration)
        : base(
            $"{configuration.GetSection("Pea.API:applicationUrl").Value}/planner/",
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

    public async Task<PlannerModel?> GetPlannerFromId(Guid id)
        => await base.GetOne<PlannerModel>("id", id);

    public async Task<List<PlannerModel>?> GetPlannerFromClassId(Guid classId)
        => await base.GetMany<PlannerModel>("class", classId);

    public async Task<HttpResponseMessage> GetPlannerPage(GetPeaPageRequest request)
        => await base.Create<GetPeaPageRequest>(request, "page");

    public async Task<HttpResponseMessage> CreatePlanner(CreatePeaRequest request)
        => await base.Create<CreatePeaRequest>(request, "create");

    public async Task<HttpResponseMessage> UpdatePlanner(UpdatePeaRequest request)
        => await base.Update(request, "update");

    public async Task<HttpResponseMessage> DeletePlanner(Guid classId)
        => await base.Delete("delete", classId);
}