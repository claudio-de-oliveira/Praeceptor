using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

using NETCore.MailKit.Core;

using PraeceptorCQRS.Application.Behaviors;
using PraeceptorCQRS.Application.Email;
using PraeceptorCQRS.Application.Entities.Institute.Events;
using PraeceptorCQRS.Domain.Email;

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

