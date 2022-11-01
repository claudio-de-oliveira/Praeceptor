using Hanssens.Net;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

using PraeceptorCQRS.Utilities;

using Serilog;

using System.IdentityModel.Tokens.Jwt;

using UserManager.App;
using UserManager.App.Components.Toaster;
using UserManager.App.Interfaces;
using UserManager.App.Services;

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

// Mapster
// builder.Services.AddMappings();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
                .AddCircuitOptions(options =>
                {
                    if (builder.Environment.IsDevelopment()) //only add details when debugging
                        options.DetailedErrors = true;
                });
builder.Services.AddScoped<ToasterService>();

Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

var storage = new LocalStorage();
builder.Services.AddSingleton<ILocalStorage>(storage);

builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddScoped<TokenProvider>();

builder.Services.AddSingleton<IUserService>(new UserService(builder.Configuration));
builder.Services.AddSingleton<IHoldingService>(new HoldingService(builder.Configuration));
builder.Services.AddSingleton<IInstituteService>(new InstituteService(builder.Configuration));
builder.Services.AddSingleton<ICourseService>(new CourseService(builder.Configuration));

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
        options.ClientId = builder.Configuration.GetSection("UserManager.APP:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("UserManager.APP:ClientSecret").Value;

        // When set to code, the middleware will use PKCE protection
        options.ResponseType = builder.Configuration.GetSection("UserManager.APP:ResponseType").Value;

        options.Scope.Clear();

        // Save the tokens we receive from the IDP
        options.SaveTokens = true;

        options.Scope.Add(builder.Configuration.GetSection("UserManager.API:Scopes").Value);

        // We need to tell the client to pull remaining claims from the UserInfo endpoint by specifying scopes
        // that the client application needs to access and setting the GetClaimsFromUserInfoEndpoint option.
        options.Scope.Add("openid");
        options.Scope.Add("profile");

        options.Scope.Add("roles");
        options.ClaimActions.MapJsonKey("role", "role", "role");
        options.TokenValidationParameters.RoleClaimType = "role";
        options.ClaimActions.MapJsonKey("instituteid", "instituteid", "instituteid");
        options.ClaimActions.MapJsonKey("gender", "gender", "gender");
        // options.Scope.Add("Institutes");
        // options.ClaimActions.MapJsonKey("institute", "institute", "institute");
        // options.TokenValidationParameters.RoleClaimType = "institute";
        // options.Scope.Add("Holdings");
        options.ClaimActions.MapJsonKey("holdingid", "holdingid", "holdingid");
        // options.TokenValidationParameters.RoleClaimType = "holding";

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

