using Microsoft.AspNetCore.Mvc.Infrastructure;

using PraeceptorCQRS.Presentation.Document.Api.Common;
using PraeceptorCQRS.Presentation.Document.Api.Common.Errors;

namespace PraeceptorCQRS.Presentation.Document.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, DocumentProblemDetailsFactory>();
            services.AddMappings();

            return services;
        }
    }
}

