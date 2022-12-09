using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using Microsoft.AspNetCore.Mvc;

using PraeceptorCQRS.Contracts.Entities.SocialBody;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services;

public class SocialBodyService : HttpAbstractService, ISocialBodyService
{
    private readonly ConfigurationManager _configuration;

    public SocialBodyService(ConfigurationManager configuration)
        : base(
            $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/socialbody/",
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

    public async Task<HttpResponseMessage> CreateSocialBodyEntry([FromBody] CreateSocialBodyEntryRequest request)
        => await base.Create<CreateSocialBodyEntryRequest>(request, "create");
    public async Task<int> GetSocialBodyEntriesCountByCourse(Guid courseId)
        => await base.Count("get", "count", courseId);
    public async Task<List<SocialBodyEntryModel>?> GetSocialBodyEntriesList(Guid courseId)
        => await base.GetMany<SocialBodyEntryModel>("get", "list", courseId);
    public async Task<HttpResponseMessage> GetSocialBodyEntriesPage([FromBody] GetSocialBodyPageRequest request)
        => await base.Create<GetSocialBodyPageRequest>(request, "get", "page");
    public async Task<SocialBodyEntryModel?> GetSocialBodyEntry(Guid courseId, Guid preceptorId, Guid roleId)
        => await base.GetOne<SocialBodyEntryModel>("get", "entry", courseId, preceptorId, roleId);
    public async Task<HttpResponseMessage> DeleteSocialBodyEntry(Guid courseId, Guid preceptorId, Guid roleId)
        => await base.Delete("delete", "entry", courseId, preceptorId, roleId);
    public async Task<HttpResponseMessage> DeleteSocialBodyByCourse(Guid courseId)
        => await base.Delete("delete", courseId);
}
