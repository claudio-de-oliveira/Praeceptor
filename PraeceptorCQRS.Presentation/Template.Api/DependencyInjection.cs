using PraeceptorCQRS.Presentation.Template.Api.Common.Errors;
using PraeceptorCQRS.Presentation.Template.Api.Common;

using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace PraeceptorCQRS.Presentation.Template.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, TemplateProblemDetailsFactory>();
            services.AddMappings();
            return services;
        }
    }
}

