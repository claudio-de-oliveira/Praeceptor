using Microsoft.AspNetCore.Mvc.Infrastructure;

using UserManager.Api.Common;
using UserManager.Api.Common.Errors;

namespace UserManager.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, UserManagerProblemDetailsFactory>();
            services.AddMappings();

            return services;
        }
    }
}
