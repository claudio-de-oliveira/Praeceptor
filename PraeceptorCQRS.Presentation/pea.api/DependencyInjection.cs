using Microsoft.AspNetCore.Mvc.Infrastructure;

using Pea.Api.Common;
using Pea.Api.Common.Errors;

namespace Pea.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, PeaProblemDetailsFactory>();
            services.AddMappings();

            return services;
        }
    }
}