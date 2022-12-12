using IdentitiServer.Api;
using IdentitiServer.Api.CustomTokenProviders;

using IdentityServer.Api;
using IdentityServer.Api.Services;

using IdentityServer4.Services;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using PraeceptorCQRS.Application.Email;
using PraeceptorCQRS.Domain.Email;
using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Infrastructure.Email;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    // .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
    .Enrich.FromLogContext()
    // uncomment to write to Azure diagnostics stream
    //.WriteTo.File(
    //    @"D:\home\LogFiles\Application\identityserver.txt",
    //    fileSizeLimitBytes: 1_000_000,
    //    rollOnFileSizeLimit: true,
    //    shared: true,
    //    flushToDiskInterval: TimeSpan.FromSeconds(1))
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
    .CreateLogger();

string? path = FindFirstFilePath("common.settings.json");

if (path is null)
{
    Console.WriteLine("O arquivo de configuração \"common.settings.json\" não foi encontrado.\nA aplicação não pode continuar.\n");
    return;
}

builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFileAfterLastJsonFile(
        Path.Combine(path, "common.settings.json"),
        optional: true,
        reloadOnChange: false);
});

// Add services to the container.

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("IdentityServerHostConnection");

var migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b =>
        b.MigrationsAssembly(migrationsAssembly)));
builder.Services.AddDbContext<PraeceptorCQRSDbContext>(options =>
   options.UseSqlServer(connectionString, b =>
        b.MigrationsAssembly(migrationsAssembly)));

// ASP.NET Core Identity Configuration
// We can register ASP.NET Core Identity with two extension methods: AddIdentityCore<TUser>
// and AddIdentity<TUser, TRole>.
// The AddIdentityCore method adds the services that are necessary for user-management actions,
// such as creating users, hashing passwords, password validation, etc. If your project doesn’t
// require any additional features, then you should use this method for the implementation.
// If your project requires those features and any additional ones like supporting Roles not only
// Users, supporting external authentication, and SingInManager, as our application does, you have
// to use the AddIdentity method.
// Of course, we can achieve the same result with the AddIdentityCore method, but then we would
// have to manually register Roles, SignInManager, Cookies, etc.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(

#region Modificações do dia 17/09/2022

    opt =>
    {
        opt.Password.RequiredLength = 7;
        opt.Password.RequireDigit = false;
        opt.Password.RequireUppercase = false;

        // Pay attention that showing a message about duplicated enail.
        // That’s because, if this is a user who tries to hack our account, we have just reviled this
        // email that already exists in our system and allowed them to focus only on the passwords.
        // A better solution would be to send an email message to the owner of this account, with the
        // information that the account already exists.
        // By doing this, we don’t narrow down hacking possibilities for the malicious user and our
        // regular user could proactively change the password or contact the system administrator to
        // report a possible account breach.
        opt.User.RequireUniqueEmail = true;

        opt.SignIn.RequireConfirmedEmail = true;
        opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
    }

    #endregion Modificações do dia 17/09/2022

    )
    .AddRoles<IdentityRole>()
    // So, next to the AddIdentity method call, we add the AddEntityFrameworkStores method to
    // register the required EF Core implementation of Identity stores.
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation");

#region Modificações do dia 17/09/2022

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

#endregion Modificações do dia 17/09/2022

// For the reset password functionality, a short period of time is quite ok, but for the email
// confirmation, it is not. A user could easily get distracted and come back to confirm its email
// after one day for example. Thus, we have to increase a lifespan for this type of token.
builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromDays(3));

// To start using these stores, you’ll need to replace any existing calls to AddInMemoryClients,
// AddInMemoryIdentityResources, AddInMemoryApiScopes, AddInMemoryApiResources,
// and AddInMemoryPersistedGrants in your ConfigureServices method in Startup.cs with
// AddConfigurationStore and AddOperationalStore.
var identityServerBuilder = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
    options.EmitStaticAudienceClaim = false;
})
    .AddAspNetIdentity<ApplicationUser>(
    )
    // this adds the config data from DB (clients, resources, CORS)
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = builder =>
            builder.UseSqlServer(connectionString, b =>
                b.MigrationsAssembly(migrationsAssembly));
    })
    // this adds the operational data from DB (codes, tokens, consents)
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = builder =>
            builder.UseSqlServer(connectionString, b =>
                b.MigrationsAssembly(migrationsAssembly));

        // this enables automatic token cleanup. this is optional.
        options.EnableTokenCleanup = true;
    })
    // not recommended for production - you need to store your key material somewhere secure
    .AddDeveloperSigningCredential(
    );

#region Modificações do dia 17/09/2022

var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

#endregion Modificações do dia 17/09/2022

builder.Services.AddTransient<IProfileService, ProfileService>();

builder.Services.AddAuthentication();

// *_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*_*
var app = builder.Build();

// this will do the initial DB population
SeedData.InitializeDatabase(app, builder.Configuration);
SeedData.EnsureSeedData(
    builder.Services,
    Guid.Empty.ToString(),
    Guid.Empty.ToString(),
    Guid.Empty.ToString()
    );
SeedData.CreateUserRoles(builder.Services).Wait();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

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

