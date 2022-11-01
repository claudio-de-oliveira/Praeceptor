using FileStream.Api.Common;
using FileStream.Api.Common.Errors;

using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FileStream.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, FileStreamDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}
