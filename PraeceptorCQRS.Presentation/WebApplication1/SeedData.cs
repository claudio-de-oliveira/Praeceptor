// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Ardalis.GuardClauses;

using IdentityModel;

using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Data;

using System.Security.Claims;

namespace IdentityServer.Api
{
    public class SeedData
    {
        public static void InitializeDatabase(IApplicationBuilder app, IConfiguration configuration)
        {
            var serviceFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            Guard.Against.Null(serviceFactory);
            using var serviceScope = serviceFactory.CreateScope();

            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            context.Database.Migrate();

            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients(configuration))
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes(configuration))
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            if (!context.ApiResources.Any())
            {
                var resources = Config.ApiResources(configuration);

                foreach (var resource in resources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }

        public static void EnsureSeedData(
            IServiceCollection services,
            string holdingId,
            string instituteId,
            string courseId
            )
        {
            services.AddLogging();

            using var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            Guard.Against.Null(context);
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var systemAdmin = userMgr.FindByNameAsync("SystemAdmin").Result;
            if (systemAdmin == null)
            {
                systemAdmin = new ApplicationUser
                {
                    UserName = "SystemAdmin",
                    Email = "clalulana@gmail.com",
                    NormalizedEmail = "CLALULANA@GMAIL.COM",
                    NormalizedUserName = "SYSTEMADMIN",
                    Gender = 'M',
                    HoldingId = holdingId,
                    InstituteId = instituteId,
                    CourseId = courseId,
                    IsEnabled = true
                };
                var result = userMgr.CreateAsync(systemAdmin, "Eaafe_301").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(systemAdmin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Gerenciador do Sistema"),
                            new Claim(JwtClaimTypes.GivenName, "Gerenciador do"),
                            new Claim(JwtClaimTypes.FamilyName, "Sistema"),
                            new Claim(JwtClaimTypes.WebSite, "http://system_admin.com"),
                            new Claim("gender", systemAdmin.Gender.ToString()),
                            new Claim("holdingid", systemAdmin.HoldingId),
                            new Claim("instituteid", systemAdmin.InstituteId),
                            new Claim("courseid", systemAdmin.CourseId)
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            var holdingAdmin = userMgr.FindByNameAsync("HoldingAdmin").Result;
            if (holdingAdmin == null)
            {
                holdingAdmin = new ApplicationUser
                {
                    UserName = "HoldingAdmin",
                    Email = "clalulana1@gmail.com",
                    NormalizedEmail = "CLALULANA1@GMAIL.COM",
                    NormalizedUserName = "HOLDINGADMIN",
                    Gender = 'M',
                    HoldingId = holdingId,
                    InstituteId = instituteId,
                    CourseId = courseId,
                    IsEnabled = true
                };
                var result = userMgr.CreateAsync(holdingAdmin, "Eaafe_301").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(holdingAdmin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Gerenciador da Holding"),
                            new Claim(JwtClaimTypes.GivenName, "Gerenciador da"),
                            new Claim(JwtClaimTypes.FamilyName, "Holding"),
                            new Claim(JwtClaimTypes.WebSite, "http://holding_admin.com"),
                            new Claim("gender", holdingAdmin.Gender.ToString()),
                            new Claim("holdingid", holdingAdmin.HoldingId),
                            new Claim("instituteid", holdingAdmin.InstituteId),
                            new Claim("courseid", holdingAdmin.CourseId)
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            var instituteAdmin = userMgr.FindByNameAsync("InstituteAdmin").Result;
            if (instituteAdmin == null)
            {
                instituteAdmin = new ApplicationUser
                {
                    UserName = "InstituteAdmin",
                    Email = "clalulana2@gmail.com",
                    NormalizedEmail = "CLALULANA2@GMAIL.COM",
                    NormalizedUserName = "INSTITUTEADMIN",
                    Gender = 'M',
                    HoldingId = holdingId,
                    InstituteId = instituteId,
                    CourseId = courseId,
                    IsEnabled = true
                };
                var result = userMgr.CreateAsync(instituteAdmin, "Eaafe_301").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(instituteAdmin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Gerenciador do Instituto"),
                            new Claim(JwtClaimTypes.GivenName, "Gerenciador do"),
                            new Claim(JwtClaimTypes.FamilyName, "Instituto"),
                            new Claim(JwtClaimTypes.WebSite, "http://institute_admin.com"),
                            new Claim("gender", instituteAdmin.Gender.ToString()),
                            new Claim("holdingid", instituteAdmin.HoldingId),
                            new Claim("instituteid", instituteAdmin.InstituteId),
                            new Claim("courseid", instituteAdmin.CourseId)
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            var courseAdmin = userMgr.FindByNameAsync("CourseAdmin").Result;
            if (courseAdmin == null)
            {
                courseAdmin = new ApplicationUser
                {
                    UserName = "CourseAdmin",
                    Email = "clalulana3@gmail.com",
                    NormalizedEmail = "CLALULANA3@GMAIL.COM",
                    NormalizedUserName = "COURSEADMIN",
                    Gender = 'F',
                    HoldingId = holdingId,
                    InstituteId = instituteId,
                    CourseId = courseId,
                    IsEnabled = true
                };
                var result = userMgr.CreateAsync(courseAdmin, "Eaafe_301").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(courseAdmin, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Gerenciador do Curso"),
                            new Claim(JwtClaimTypes.GivenName, "Gerenciador do"),
                            new Claim(JwtClaimTypes.FamilyName, "Curso"),
                            new Claim(JwtClaimTypes.WebSite, "http://course_admin.com"),
                            new Claim("gender", courseAdmin.Gender.ToString()),
                            new Claim("holdingid", courseAdmin.HoldingId),
                            new Claim("instituteid", courseAdmin.CourseId),
                            new Claim("courseid", courseAdmin.InstituteId)
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        public static async Task CreateUserRoles(IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Visitor", "SystemAdmin", "HoldingAdmin", "InstituteAdmin", "CourseAdmin", "Coordenador", "Consultor", "Pedagogo", "Professor" };

            foreach (var role in roles)
            {
                bool adminRoleExists = await RoleManager.RoleExistsAsync(role);
                if (!adminRoleExists)
                    _ = await RoleManager.CreateAsync(new IdentityRole(role));
            }

            ApplicationUser userToMakeAdmin;

            userToMakeAdmin = await UserManager.FindByNameAsync("SystemAdmin");
            await UserManager.AddToRoleAsync(userToMakeAdmin, "SystemAdmin");
            await UserManager.AddToRoleAsync(userToMakeAdmin, "Visitor");

            userToMakeAdmin = await UserManager.FindByNameAsync("HoldingAdmin");
            await UserManager.AddToRoleAsync(userToMakeAdmin, "HoldingAdmin");
            await UserManager.AddToRoleAsync(userToMakeAdmin, "Visitor");

            userToMakeAdmin = await UserManager.FindByNameAsync("InstituteAdmin");
            await UserManager.AddToRoleAsync(userToMakeAdmin, "InstituteAdmin");
            await UserManager.AddToRoleAsync(userToMakeAdmin, "Visitor");

            userToMakeAdmin = await UserManager.FindByNameAsync("CourseAdmin");
            await UserManager.AddToRoleAsync(userToMakeAdmin, "CourseAdmin");
            await UserManager.AddToRoleAsync(userToMakeAdmin, "Visitor");
        }
    }
}