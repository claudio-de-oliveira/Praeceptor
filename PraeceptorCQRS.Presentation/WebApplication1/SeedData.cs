// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Ardalis.GuardClauses;

using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using PraeceptorCQRS.Domain.Entities;
using PraeceptorCQRS.Infrastructure.Data;

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

        public static void EnsureSeedData(IServiceCollection services)
        {
            services.AddLogging();

            using var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            Guard.Against.Null(context);
            context.Database.Migrate();

            // var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            // var systemAdmin = userMgr.FindByNameAsync("SystemManager").Result;
            // if (systemAdmin == null)
            // {
            //     systemAdmin = new ApplicationUser
            //     {
            //         UserName = "SystemManager",
            //         Email = "systemmanager@gmail.com",
            //         NormalizedEmail = "SYSTEMMANAGER@GMAIL.COM",
            //         NormalizedUserName = "SYSTEMMANAGER",
            //         Gender = 'M',
            //         HoldingId = Guid.Empty.ToString(), // "holdingId"
            //         InstituteId = Guid.Empty.ToString(), // "instituteId"
            //         CourseId = Guid.Empty.ToString(), // "courseId"
            //         IsEnabled = true
            //     };
            //     var result = userMgr.CreateAsync(systemAdmin, "@Eaafe_301").Result;
            //     if (!result.Succeeded)
            //     {
            //         throw new Exception(result.Errors.First().Description);
            //     }
            // 
            //     result = userMgr.AddClaimsAsync(systemAdmin, new Claim[]{
            //                 new Claim(JwtClaimTypes.Name, "Gerenciador do Sistema"),
            //                 new Claim(JwtClaimTypes.GivenName, "Gerenciador do"),
            //                 new Claim(JwtClaimTypes.FamilyName, "Sistema"),
            //                 new Claim(JwtClaimTypes.WebSite, "http://claudio.com"),
            //                 new Claim("gender", systemAdmin.Gender.ToString()),
            //                 new Claim("holdingid", systemAdmin.HoldingId),
            //                 new Claim("instituteid", systemAdmin.InstituteId),
            //                 new Claim("courseid", systemAdmin.CourseId)
            //             }).Result;
            //     if (!result.Succeeded)
            //     {
            //         throw new Exception(result.Errors.First().Description);
            //     }
            //     Log.Debug("SystemAdmin created");
            // }
            // else
            // {
            //     Log.Debug("SystemAdmin already exists");
            // }
            // 
            // var holdingAdmin = userMgr.FindByNameAsync("HoldingManager").Result;
            // if (holdingAdmin == null)
            // {
            //     holdingAdmin = new ApplicationUser
            //     {
            //         UserName = "HoldingManager",
            //         Email = "holdingmanager@gmail.com",
            //         NormalizedEmail = "HOLDINGMANAGER@GMAIL.COM",
            //         NormalizedUserName = "HOLDINGMANAGER",
            //         Gender = 'M',
            //         HoldingId = Guid.Empty.ToString(), // "holdingId"
            //         InstituteId = Guid.Empty.ToString(), // "instituteId"
            //         CourseId = Guid.Empty.ToString(), // "courseId"
            //         IsEnabled = true
            //     };
            //     var result = userMgr.CreateAsync(holdingAdmin, "@Eaafe_301").Result;
            //     if (!result.Succeeded)
            //     {
            //         throw new Exception(result.Errors.First().Description);
            //     }
            // 
            //     result = userMgr.AddClaimsAsync(holdingAdmin, new Claim[]{
            //                 new Claim(JwtClaimTypes.Name, "Gerenciador da Holding"),
            //                 new Claim(JwtClaimTypes.GivenName, "Gerenciador da"),
            //                 new Claim(JwtClaimTypes.FamilyName, "Holding"),
            //                 new Claim(JwtClaimTypes.WebSite, "http://claudio.com"),
            //                 new Claim("gender", holdingAdmin.Gender.ToString()),
            //                 new Claim("holdingid", holdingAdmin.HoldingId),
            //                 new Claim("instituteid", holdingAdmin.InstituteId),
            //                 new Claim("courseid", holdingAdmin.CourseId)
            //             }).Result;
            //     if (!result.Succeeded)
            //     {
            //         throw new Exception(result.Errors.First().Description);
            //     }
            //     Log.Debug("HoldingAdmin created");
            // }
            // else
            // {
            //     Log.Debug("HoldingAdmin already exists");
            // }
            // 
            // var instituteAdmin = userMgr.FindByNameAsync("InstituteManager").Result;
            // if (instituteAdmin == null)
            // {
            //     instituteAdmin = new ApplicationUser
            //     {
            //         UserName = "InstituteManager",
            //         Email = "institutemanager@gmail.com",
            //         NormalizedEmail = "INSTITUTEMANAGER@GMAIL.COM",
            //         NormalizedUserName = "INSTITUTEMANAGER",
            //         Gender = 'M',
            //         HoldingId = Guid.Empty.ToString(), // "holdingId"
            //         InstituteId = Guid.Empty.ToString(), // "instituteId"
            //         CourseId = Guid.Empty.ToString(), // "courseId"
            //         IsEnabled = true
            //     };
            //     var result = userMgr.CreateAsync(instituteAdmin, "@Eaafe_301").Result;
            //     if (!result.Succeeded)
            //     {
            //         throw new Exception(result.Errors.First().Description);
            //     }
            // 
            //     result = userMgr.AddClaimsAsync(instituteAdmin, new Claim[]{
            //                 new Claim(JwtClaimTypes.Name, "Gerenciador do Instituto"),
            //                 new Claim(JwtClaimTypes.GivenName, "Gerenciador do"),
            //                 new Claim(JwtClaimTypes.FamilyName, "Instituto"),
            //                 new Claim(JwtClaimTypes.WebSite, "http://claudio.com"),
            //                 new Claim("gender", instituteAdmin.Gender.ToString()),
            //                 new Claim("holdingid", instituteAdmin.HoldingId),
            //                 new Claim("instituteid", instituteAdmin.InstituteId),
            //                 new Claim("courseid", instituteAdmin.CourseId)
            //             }).Result;
            //     if (!result.Succeeded)
            //     {
            //         throw new Exception(result.Errors.First().Description);
            //     }
            //     Log.Debug("InstituteAdmin created");
            // }
            // else
            // {
            //     Log.Debug("InstituteAdmin already exists");
            // }

            /*
            var luisa = userMgr.FindByNameAsync("Luisa").Result;
            if (luisa == null)
            {
                luisa = new ApplicationUser
                {
                    UserName = "Luisa",
                    Email = "luisa.bonin@gmail.com",
                    NormalizedEmail = "luisa.bonin@gmail.com",
                    NormalizedUserName = "Luisa Bonin de Oliveira",
                    Gender = 'F',
                    HoldingId = Guid.Empty.ToString(), // "holdingId"
                    InstituteId = Guid.Empty.ToString(), // "instituteId"
                    CourseId = Guid.Empty.ToString(), // "courseId"
                    IsEnabled = true
                };
                var result = userMgr.CreateAsync(luisa, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
        
                result = userMgr.AddClaimsAsync(luisa, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Luisa Bonin de Oliveira"),
                            new Claim(JwtClaimTypes.GivenName, "Luisa Bonin de"),
                            new Claim(JwtClaimTypes.FamilyName, "Oliveira"),
                            new Claim(JwtClaimTypes.WebSite, "http://luisa.com"),
                            new Claim("gender", luisa.Gender.ToString()),
                            new Claim("holdingid", luisa.HoldingId),
                            new Claim("courseid", luisa.InstituteId),
                            new Claim("instituteid", luisa.CourseId)
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("luisa created");
            }
            else
            {
                Log.Debug("luisa already exists");
            }
            */
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

            // ApplicationUser user;
            // 
            // user = await UserManager.FindByNameAsync("SystemManager");
            // await UserManager.AddToRoleAsync(user, "SystemAdmin");
            // 
            // user = await UserManager.FindByNameAsync("HoldingManager");
            // await UserManager.AddToRoleAsync(user, "HoldingAdmin");
            // 
            // user = await UserManager.FindByNameAsync("InstituteManager");
            // await UserManager.AddToRoleAsync(user, "InstituteAdmin");

            // user = await UserManager.FindByNameAsync("Luisa");
            // await UserManager.AddToRoleAsync(user, "Pedagogo");
        }
    }
}
