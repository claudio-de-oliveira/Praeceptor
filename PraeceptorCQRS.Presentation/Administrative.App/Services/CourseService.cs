using Administrative.App.Interfaces;
using Administrative.App.Models;

using IdentityModel.Client;

using PraeceptorCQRS.Contracts.Entities.Course;
using PraeceptorCQRS.Utilities;

using static IdentityModel.OidcConstants;

namespace Administrative.App.Services;

public class CourseService : HttpAbstractService, ICourseService
{
    private readonly ConfigurationManager _configuration;

    public CourseService(ConfigurationManager configuration)
        : base(
            $"{configuration.GetSection("Administrative.API:applicationUrl").Value}/course/",
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

    public async Task<HttpResponseMessage> CreateCourse(CreateCourseRequest request)
        => await base.Create<CreateCourseRequest>(request, "create");
    public async Task<HttpResponseMessage> DeleteCourse(Guid id)
        => await base.Delete("delete", id);

    public async Task<CourseModel?> GetCourseById(Guid id)
    {
        var item = await base.GetOne<CourseModel>("get", "id", id);
        if (item is null)
            return null;
        return item;
    }
    public async Task<CourseModel?> GetCourseByCode(string code)
    {
        var item = await base.GetOne<CourseModel>("get", "code", code);
        if (item is null)
            return null;
        return item;
    }

    public async Task<List<CurriculumModel>> GetCurriculumsByCourseId(Guid id)
        => await base.GetMany<CurriculumModel>("list", "curriculum", id) ?? new();
    public async Task<int> GetCourseCount(Guid instituteId)
        => await base.Count("get", "count", instituteId);
    public async Task<HttpResponseMessage> PostPage(GetCoursePageRequest request)
        => await base.Create<GetCoursePageRequest>(request, "get", "page");
    public async Task<HttpResponseMessage> UpdateCourse(UpdateCourseRequest request)
        => await base.Update(request, "update");
}
