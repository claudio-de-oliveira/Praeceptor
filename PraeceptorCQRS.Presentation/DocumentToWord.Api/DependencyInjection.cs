using DocumentToWord.Api.Common.Errors;
using DocumentToWord.Api.Common;

using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DocumentToWord.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, DocumentToWordDetailsFactory>();
            services.AddMappings();

            return services;
        }
    }
}