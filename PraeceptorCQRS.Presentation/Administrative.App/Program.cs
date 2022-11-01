using Administrative.App;
using Administrative.App.Components.Toaster;
using Administrative.App.Interfaces;
using Administrative.App.Services;

using Hanssens.Net;

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
    Console.WriteLine("O arquivo de configuração \"common.settings.json\" não foi encontrado.\nA aplicação não pode continuar.\n");
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

var storage = new LocalStorage();
builder.Services.AddSingleton<ILocalStorage>(storage);
builder.Services.AddSingleton<IAdminService>(new AdminService(builder.Configuration));
builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddScoped<TokenProvider>();

var axisTypeService = new AxisTypeService(builder.Configuration);
builder.Services.AddSingleton<IAxisTypeService>(axisTypeService);
var componentService = new ComponentService(builder.Configuration);
builder.Services.AddSingleton<IComponentService>(componentService);
var classService = new ClassService(builder.Configuration);
builder.Services.AddSingleton<IClassService>(classService);
var classTypeService = new ClassTypeService(builder.Configuration);
builder.Services.AddSingleton<IClassTypeService>(classTypeService);
var courseService = new CourseService(builder.Configuration);
builder.Services.AddSingleton<ICourseService>(courseService);
var holdingService = new HoldingService(builder.Configuration);
builder.Services.AddSingleton<IHoldingService>(holdingService);
var instituteService = new InstituteService(builder.Configuration);
builder.Services.AddSingleton<IInstituteService>(instituteService);
var preceptorService = new PreceptorService(builder.Configuration);
builder.Services.AddSingleton<IPreceptorService>(preceptorService);
var preceptorDegreeService = new PreceptorDegreeService(builder.Configuration);
builder.Services.AddSingleton<IPreceptorDegreeService>(preceptorDegreeService);
var preceptorRegimeService = new PreceptorRegimeService(builder.Configuration);
builder.Services.AddSingleton<IPreceptorRegimeService>(preceptorRegimeService);

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
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        options.ExpireTimeSpan = TimeSpan.FromMinutes(1)
        )
    // Finally, AddOpenIdConnect is used to configure the handler that performs the OpenID Connect protocol. The
    // Authority indicates where the trusted token service is located. We then identify this client via the ClientId and
    // the ClientSecret. SaveTokens is used to persist the tokens from IdentityServer in the cookie.
    .AddOpenIdConnect(options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
        options.Authority = builder.Configuration.GetSection("IdentityServer:Authority").Value;
        options.ClientId = builder.Configuration.GetSection("Administrative.APP:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("Administrative.APP:ClientSecret").Value;

        // When set to code, the middleware will use PKCE protection
        options.ResponseType = builder.Configuration.GetSection("Administrative.APP:ResponseType").Value;

        options.Scope.Clear();

        // Save the tokens we receive from the IDP
        options.SaveTokens = true;

        options.Scope.Add(builder.Configuration.GetSection("Administrative.API:Scopes").Value);

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
#endregion

var app = builder.Build();

await SeedData.InitializeHoldingTable(holdingService);
Console.WriteLine("Passou pelas Holdings");
await SeedData.InitializeInstituteTable(instituteService);
Console.WriteLine("Passou pelos Institutos");
await SeedData.InitializeCourseTable(courseService);
Console.WriteLine("Passou pelos Cursos");
await SeedData.InitializeClassTypeTable(classTypeService);
Console.WriteLine("Passou pelos Tipos de Disciplinas");
await SeedData.InitializeClassTable(classService);
Console.WriteLine("Passou pelas Disciplinas");
await SeedData.InitializePreceptorDegreeTable(preceptorDegreeService);
Console.WriteLine("Passou pelas Titulações");
await SeedData.InitializePreceptorRegimeTable(preceptorRegimeService);
Console.WriteLine("Passou pelos Regimes");
await SeedData.InitializePreceptorTable(preceptorService);
Console.WriteLine("Passou pelos Professores");
await SeedData.InitializeAxisTypeTable(axisTypeService);
Console.WriteLine("Passou pelos Eixos");
await SeedData.InitializeSyllabusTable(componentService);
Console.WriteLine("Passou pelos Componentes");

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

