using Document.App;
using Document.App.Components.Toaster;
using Document.App.Download;
using Document.App.DownloadFile.DependencyInjection;
using Document.App.Interfaces;
using Document.App.Notifiers;
using Document.App.SeedData.Documents;
using Document.App.SeedData.Images;
using Document.App.SeedData.Planners;
using Document.App.SeedData.SimpleTables;
using Document.App.SeedData.Variables;
using Document.App.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

using PraeceptorCQRS.Utilities;

using Serilog;

using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

string? path = FindFirstFilePath("common.settings.json");

if (path is null)
{
    Console.WriteLine("O arquivo de configura��o \"common.settings.json\" n�o foi encontrado.\nA aplica��o n�o pode continuar.\n");
    return;
}

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFileAfterLastJsonFile(
        Path.Combine(path, "common.settings.json"),
        optional: true,
        reloadOnChange: false);
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
                .AddCircuitOptions(options =>
                 {
                     if (builder.Environment.IsDevelopment()) // only add details when debugging
                         options.DetailedErrors = true;
                 });
builder.Services.AddScoped<ToasterService>();

Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

builder.Services.AddSingleton(new DocumentNavigationComponentNotifier());
builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddScoped<TokenProvider>();

builder.Services.AddBlazorDownloadFile();

InstituteService instituteService = new(builder.Configuration);
builder.Services.AddSingleton<IInstituteService>(instituteService);
FileStreamService fileStreamService = new(builder.Configuration);
builder.Services.AddSingleton<IFileStreamService>(fileStreamService);
DocxStreamService docxStreamService = new(builder.Configuration);
builder.Services.AddSingleton<IDocxStreamService>(docxStreamService);
DocumentListService documentService = new(builder.Configuration);
builder.Services.AddSingleton<IDocumentListService>(documentService);
ChapterListService chapterService = new(builder.Configuration);
builder.Services.AddSingleton<IChapterListService>(chapterService);
SectionListService sectionService = new(builder.Configuration);
builder.Services.AddSingleton<ISectionListService>(sectionService);
SubSectionListService subSectionService = new(builder.Configuration);
builder.Services.AddSingleton<ISubSectionListService>(subSectionService);
SubSubSectionListService subSubSectionService = new(builder.Configuration);
builder.Services.AddSingleton<ISubSubSectionListService>(subSubSectionService);
GroupService groupService = new(builder.Configuration);
builder.Services.AddSingleton<IGroupService>(groupService);
GroupValueService groupValueService = new(builder.Configuration);
builder.Services.AddSingleton<IGroupValueService>(groupValueService);
VariableService variableService = new(builder.Configuration);
builder.Services.AddSingleton<IVariableService>(variableService);
VariableValueService variableValueService = new(builder.Configuration);
builder.Services.AddSingleton<IVariableValueService>(variableValueService);
SimpleTableService simpleTableService = new(builder.Configuration);
builder.Services.AddSingleton<ISimpleTableService>(simpleTableService);
CourseService courseService = new(builder.Configuration);
builder.Services.AddSingleton<ICourseService>(courseService);

var classService = new ClassService(builder.Configuration);
builder.Services.AddSingleton<IClassService>(classService);
var classTypeService = new ClassTypeService(builder.Configuration);
builder.Services.AddSingleton<IClassTypeService>(classTypeService);
var plannerService = new PlannerService(builder.Configuration);
builder.Services.AddSingleton<IPlannerService>(plannerService);

var wordService = new WordService(builder.Configuration);
builder.Services.AddSingleton<IWordService>(wordService);

#region IdentityServer4

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
{
    // We are using a cookie to locally sign-in the user (via "Cookies" as the DefaultScheme), and we set the ...
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // ... DefaultChallengeScheme to oidc because when we need the user to login, we will be using the OpenID Connect protocol.
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    // We then use AddCookie to add the handler that can process cookies.
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => options.ExpireTimeSpan = TimeSpan.FromMinutes(1))
    // Finally, AddOpenIdConnect is used to configure the handler that performs the OpenID Connect protocol. The
    // Authority indicates where the trusted token service is located. We then identify this client via the ClientId and
    // the ClientSecret. SaveTokens is used to persist the tokens from IdentityServer in the cookie.
    .AddOpenIdConnect(options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
        options.Authority = builder.Configuration.GetSection("IdentityServer:Authority").Value;
        options.ClientId = builder.Configuration.GetSection("Document.APP:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("Document.APP:ClientSecret").Value;

        // When set to code, the middleware will use PKCE protection
        options.ResponseType = builder.Configuration.GetSection("Document.APP:ResponseType").Value;

        options.Scope.Clear();

        // Save the tokens we receive from the IDP
        options.SaveTokens = true;

        options.Scope.Add(builder.Configuration.GetSection("Document.API:Scopes").Value);

        // We need to tell the client to pull remaining claims from the UserInfo endpoint by specifying scopes
        // that the client application needs to access and setting the GetClaimsFromUserInfoEndpoint option.
        options.Scope.Add("openid");
        options.Scope.Add("profile");

        options.Scope.Add("roles");
        options.ClaimActions.MapJsonKey("role", "role", "role");
        options.TokenValidationParameters.RoleClaimType = "role";
        options.ClaimActions.MapJsonKey("instituteid", "instituteid", "instituteid");
        options.ClaimActions.MapJsonKey("gender", "gender", "gender");
        options.ClaimActions.MapJsonKey("holdingid", "holdingid", "holdingid");
        options.ClaimActions.MapJsonKey("courseid", "courseid", "courseid");

        // It's recommended to always get claims from the UserInfoEndpoint during the flow.
        options.GetClaimsFromUserInfoEndpoint = true;

        options.Events = new OpenIdConnectEvents
        {
            OnRemoteFailure = context =>
            {
                context.Response.Redirect("/");
                context.HandleResponse();

                return Task.FromResult(0);
            }
        };
    });

#endregion IdentityServer4

var app = builder.Build();

// await Initialize(
//     Guid.Parse("758C52BE-159D-402D-767E-08DAD959E72F"),
//     fileStreamService, simpleTableService, classService, plannerService,
//     documentService, chapterService, sectionService, subSectionService, subSubSectionService,
//     groupService, groupValueService, variableService, variableValueService
//     );

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// Adds the authentication middleware to the pipeline so authentication will be performed
// automatically on every call into the host.
app.UseAuthentication();
// Adds the authorization middleware to make sure, our API endpoint cannot be accessed
// by anonymous clients.
app.UseAuthorization();

app.UseCookiePolicy(new CookiePolicyOptions
{
    Secure = CookieSecurePolicy.Always
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

// *_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*

static string? FindFirstFilePath(string filename)
{
    var folders = AppDomain.CurrentDomain.BaseDirectory.Split('\\');
    string path = "";

    for (int i = 0; i < folders.Length; i++)
    {
        path = Path.Combine(path, folders[i]);
        if (File.Exists(Path.Combine(path, filename)))
            return path;
    }

    return null;
}

#pragma warning disable CS8321 // A função local foi declarada, mas nunca usada
static async Task Initialize(
    Guid instituteId,
    FileStreamService fileStreamService,
    SimpleTableService simpleTableService,
    ClassService classService,
    PlannerService plannerService,
    DocumentListService documentService,
    ChapterListService chapterService,
    SectionListService sectionService,
    SubSectionListService subSectionService,
    SubSubSectionListService subSubSectionService,
    GroupService groupService,
    GroupValueService groupValueService,
    VariableService variableService,
    VariableValueService variableValueService
    )
{
    await InitializeAdministationTable.Initialize(instituteId, simpleTableService);

    await InitializeSecurityTable.Initialize(instituteId, simpleTableService);

    await InitializePeaTable.Initialize(instituteId, classService, plannerService);

    await InitializeImageTable.Initialize(instituteId, fileStreamService);

    await InitializeDocumentTable.Initialize(
        instituteId,
        documentService,
        chapterService,
        sectionService,
        subSectionService,
        subSubSectionService
        );

    await InitializeVariableTables.Initialize(
        instituteId,
        groupService,
        groupValueService,
        variableService,
        variableValueService
        );
}
#pragma warning restore CS8321 // A função local foi declarada, mas nunca usada