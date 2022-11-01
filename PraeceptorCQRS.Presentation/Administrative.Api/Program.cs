using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;

using PraeceptorCQRS.Application;
using PraeceptorCQRS.Application.Email;
using PraeceptorCQRS.Domain.Email;
using PraeceptorCQRS.Infrastructure;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Infrastructure.Email;
using PraeceptorCQRS.Infrastructure.Interceptors;
using PraeceptorCQRS.Presentation.Administrative.Api;
using PraeceptorCQRS.Utilities;

using Quartz;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

string? path = FindFirstFilePath("common.settings.json");

if (path is null)
{
    Console.WriteLine("O arquivo de configuração \"common.settings.json\" não foi encontrado.\nA aplicação não pode continuar.\n");
    return -1;
}

builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFileAfterLastJsonFile(
        Path.Combine(path, "common.settings.json"),
        optional: true,
        reloadOnChange: false);
});

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
    // .AddDataBase(builder.Configuration);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
        theme: SystemConsoleTheme.Colored
        )
    .CreateLogger();

#region Adds the authentication services to DI and configures Bearer as the default scheme.
var Authority = builder.Configuration.GetSection("IdentityServer").GetSection("Authority").Value;
var Audience = builder.Configuration.GetSection("Administrative.API").GetSection("Audience").Value;

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = Authority;
        options.Audience = Audience;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadPolice", policy =>
          policy.RequireClaim("scope", "Administrative.API.Read", "Administrative.API.FullAccess"));
    options.AddPolicy("CreatePolice", policy =>
          policy.RequireClaim("scope", "Administrative.API.Create", "Administrative.API.FullAccess"));
    options.AddPolicy("UpdatePolice", policy =>
          policy.RequireClaim("scope", "Administrative.API.Update", "Administrative.API.FullAccess"));
    options.AddPolicy("DeletePolice", policy =>
          policy.RequireClaim("scope", "Administrative.API.Delete", "Administrative.API.FullAccess"));
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseRouting();

// Adds the authentication middleware to the pipeline so authentication will be performed
// automatically on every call into the host.
app.UseAuthentication();
// Adds the authorization middleware to make sure, our API endpoint cannot be accessed
// by anonymous clients.
app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting host...");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
    return -1;
}
finally
{
    Log.CloseAndFlush();
}

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
