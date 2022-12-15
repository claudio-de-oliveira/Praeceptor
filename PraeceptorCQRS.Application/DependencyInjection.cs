using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using PraeceptorCQRS.Application.Behaviors;

using System.Reflection;

namespace PraeceptorCQRS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(DependencyInjection).Assembly
                );
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>)
            );

            services.AddValidatorsFromAssembly(
                Assembly.GetExecutingAssembly()
                );

            return services;
        }
    }
}

