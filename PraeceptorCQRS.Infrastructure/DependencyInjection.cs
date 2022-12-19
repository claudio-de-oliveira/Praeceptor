using Ardalis.GuardClauses;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using PraeceptorCQRS.Application.Email;
using PraeceptorCQRS.Application.Persistence;
using PraeceptorCQRS.Application.Services;
using PraeceptorCQRS.Domain.Email;
using PraeceptorCQRS.Infrastructure.BackgroundJobs;
using PraeceptorCQRS.Infrastructure.Data;
using PraeceptorCQRS.Infrastructure.Email;
using PraeceptorCQRS.Infrastructure.Interceptors;
using PraeceptorCQRS.Infrastructure.Options;
using PraeceptorCQRS.Infrastructure.Persistence;
using PraeceptorCQRS.Infrastructure.Services;

using Quartz;

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

using System;
using System.Reflection;

namespace PraeceptorCQRS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            InstallSerilog(configuration);

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IFileStreamRepository, FileStreamRepository>(o => new FileStreamRepository(configuration.GetConnectionString("FileStreamConnection")));
            services.AddScoped<IDocxStreamRepository, DocxStreamRepository>(o => new DocxStreamRepository(configuration.GetConnectionString("DocxStreamConnection")));
            services.AddScoped<ISubSubSectionRepository, SubSubSectionRepository>();
            services.AddScoped<ISubSectionRepository, SubSectionRepository>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<IChapterRepository, ChapterRepository>();
            services.AddScoped<IPreceptorRoleTypeRepository, PreceptorRoleTypeRepository>();
            services.AddScoped<IPreceptorDegreeTypeRepository, PreceptorDegreeTypeRepository>();
            services.AddScoped<IPreceptorRegimeTypeRepository, PreceptorRegimeTypeRepository>();
            services.AddScoped<IClassTypeRepository, ClassTypeRepository>();
            services.AddScoped<IPreceptorRepository, PreceptorRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IInstituteRepository, InstituteRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IHoldingRepository, HoldingRepository>();
            services.AddScoped<IListRepository, ListRepository>();
            services.AddScoped<IPeaRepository, PeaRepository>();
            services.AddScoped<ISocialBodyRepository, SocialBodyRepository>();
            services.AddScoped<ISimpleTableRepository, SimpleTableRepository>();

            services.AddScoped<IVariableXRepository, VariableXRepository>();

            services.AddScoped<IAxisTypeRepository, AxisTypeRepository>();
            services.AddScoped<IComponentRepository, ComponentRepository>();
            services.AddScoped<IEmailSender, EmailSender>();

            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            // https://www.youtube.com/watch?v=bN57EDYD6M0&t=476s
            services.ConfigureOptions<DatabaseOptionsSetup>();
            services.AddDbContext<PraeceptorCQRSDbContext>(
                (serviceProvider, dbContextOptionsBuilder) =>
                {
                    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

                    var inteceptor = serviceProvider.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
                    Guard.Against.Null(inteceptor);

                    dbContextOptionsBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
                    {
                        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                    })
                    .AddInterceptors(inteceptor);

                    dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                    dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                });

            services.ConfigureOptions<AuthenticationOptionsSetup>();
            services.AddDbContext<ApplicationDbContext>(
                (serviceProvider, dbContextOptionsBuilder) =>
                {
                    var databaseOptions = serviceProvider.GetService<IOptions<AuthenticationOptions>>()!.Value;

                    dbContextOptionsBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
                    {
                        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                    });

                    dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                    dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                });

            services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                configure
                    .AddJob<ProcessOutboxMessagesJob>(jobKey)
                    .AddTrigger(
                        trigger =>
                            trigger.ForJob(jobKey)
                                .WithSimpleSchedule(
                                    schedule =>
                                        schedule.WithIntervalInSeconds(10)
                                            .RepeatForever()));

                configure.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddQuartzHostedService();

            return services;
        }

        public static IServiceCollection AddDataBase(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var migrationsAssembly = typeof(PraeceptorCQRSDbContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<PraeceptorCQRSDbContext>(options =>
               options.UseSqlServer(connectionString, b =>
                    b.MigrationsAssembly(migrationsAssembly)));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<PraeceptorCQRSDbContext>();
            Guard.Against.Null(context);

            context.Database.EnsureCreatedAsync().GetAwaiter().GetResult();

            return services;
        }

        private static void InstallSerilog(ConfigurationManager _)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                    theme: SystemConsoleTheme.Colored
                    )
                .CreateLogger();
        }
    }
}