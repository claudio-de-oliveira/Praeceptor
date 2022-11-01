using PraeceptorCQRS.Application;
using PraeceptorCQRS.Infrastructure;
using PraeceptorCQRS.Presentation.Template.Api;

using Serilog;

using PraeceptorCQRS.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.MapControllers();

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

