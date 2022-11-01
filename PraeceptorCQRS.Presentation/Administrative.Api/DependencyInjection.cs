using Microsoft.AspNetCore.Mvc.Infrastructure;

using PraeceptorCQRS.Presentation.Administrative.Api.Common;
using PraeceptorCQRS.Presentation.Administrative.Api.Common.Errors;

namespace PraeceptorCQRS.Presentation.Administrative.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, AdministrativeProblemDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}

