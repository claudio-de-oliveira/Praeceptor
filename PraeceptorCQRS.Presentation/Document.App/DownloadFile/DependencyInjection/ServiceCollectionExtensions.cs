using Document.App.DownloadFile.Interfaces;
using Document.App.DownloadFile.Services;

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;

namespace Document.App.DownloadFile.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the Blazor Download file Service
        /// </summary>
        public static IServiceCollection AddBlazorDownloadFile(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Scoped
            )
        {
            return ServiceCollectionDescriptorExtensions.Add(services,
                new ServiceDescriptor(typeof(IDownloadFileService),
                sp => new DownloadFileService(sp.GetRequiredService<IJSRuntime>()),
                lifetime));
        }
    }
}