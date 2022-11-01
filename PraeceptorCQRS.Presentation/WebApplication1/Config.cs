// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;

using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer.Api
{
    public static class Config
    {
        // Similar to OAuth 2.0, OpenID Connect also uses the scopes concept.
        // Again, scopes represent something you want to protect and that clients
        // want to access. In contrast to OAuth, scopes in OIDC don’t represent
        // APIs, but identity data like user id, name or email address.

        // Identity resources are data like user id/name, or email address of a user.
        // An identity resource has a unique name, and we can assign arbitrary claim
        // types to it
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.Profile(),
                new IdentityResources.OpenId(),
                // Add this line
                new IdentityResource(
                    name: "roles",
                    userClaims: new[] { "role" }
                    )
            };

        // To allow clients to request access tokens for APIs, you need to define
        // API resources and register them as a scope
        public static IEnumerable<ApiResource> ApiResources(IConfiguration configuration)
        {
            return new ApiResource[]
            {
                new ApiResource(
                    configuration.GetSection("UserManager.API:audience").Value,
                    configuration.GetSection("UserManager.API:description").Value,
                    userClaims: new [] { JwtClaimTypes.Name }
                    ),
                new ApiResource(
                    configuration.GetSection("Administrative.API:audience").Value,
                    configuration.GetSection("Administrative.API:description").Value,
                    userClaims: new [] { JwtClaimTypes.Name }
                    ),
                new ApiResource(
                    configuration.GetSection("Document.API:audience").Value,
                    configuration.GetSection("Document.API:description").Value,
                    userClaims: new [] { JwtClaimTypes.Name }
                    ),
                new ApiResource(
                    configuration.GetSection("FileStream.API:audience").Value,
                    configuration.GetSection("FileStream.API:description").Value,
                    userClaims: new [] { JwtClaimTypes.Name }
                    ),
                new ApiResource(
                    configuration.GetSection("Pea.API:audience").Value,
                    configuration.GetSection("Pea.API:description").Value,
                    userClaims: new [] { JwtClaimTypes.Name }
                    ),
            };
        }

        public static IEnumerable<ApiScope> ApiScopes(IConfiguration configuration)
        {
            List<ApiScope> scopes = new();

            var userScopes = configuration.GetSection("UserManager.API:Scopes").Get<string[]>();
            foreach (var scope in userScopes)
                if (scopes.Find(x => x.Name == scope) is null)
                    scopes.Add(new ApiScope(scope));

            var documentScopes = configuration.GetSection("Document.API:Scopes").Get<string[]>();
            foreach (var scope in documentScopes)
                if (scopes.Find(x => x.Name == scope) is null)
                    scopes.Add(new ApiScope(scope));

            var holdingScopes = configuration.GetSection("Administrative.API:Scopes").Get<string[]>();
            foreach (var scope in holdingScopes)
                if (scopes.Find(x => x.Name == scope) is null)
                    scopes.Add(new ApiScope(scope));

            var fileStreamScopes = configuration.GetSection("FileStream.API:Scopes").Get<string[]>();
            foreach (var scope in fileStreamScopes)
                if (scopes.Find(x => x.Name == scope) is null)
                    scopes.Add(new ApiScope(scope));

            var peaScopes = configuration.GetSection("Pea.API:Scopes").Get<string[]>();
            foreach (var scope in peaScopes)
                if (scopes.Find(x => x.Name == scope) is null)
                    scopes.Add(new ApiScope(scope));

            return scopes;
        }

        // Clients represent applications that can request tokens from your Identity Server
        public static IEnumerable<Client> Clients(IConfiguration configuration)
        {
            var userAllowedScopes = configuration.GetSection("UserManager.APP:allowedScopes").Get<string[]>().ToList();
            userAllowedScopes.Add(IdentityServerConstants.StandardScopes.OpenId);
            userAllowedScopes.Add(IdentityServerConstants.StandardScopes.Profile);
            var documentAllowedScopes = configuration.GetSection("Document.APP:allowedScopes").Get<string[]>().ToList();
            documentAllowedScopes.Add(IdentityServerConstants.StandardScopes.OpenId);
            documentAllowedScopes.Add(IdentityServerConstants.StandardScopes.Profile);
            var administrativeAllowedScopes = configuration.GetSection("Administrative.APP:allowedScopes").Get<string[]>().ToList();
            administrativeAllowedScopes.Add(IdentityServerConstants.StandardScopes.OpenId);
            administrativeAllowedScopes.Add(IdentityServerConstants.StandardScopes.Profile);

            return new Client[]
            {
                #region No interactive user, use the clientid/secret for authentication
                #endregion

                #region No interactive API client
                // new Client
                // {
                //     ClientId = configuration.GetSection("TopologyAPI:audience").Value,
                //     ClientName = configuration.GetSection("TopologyAPI:description").Value,
                //     // secret for authentication
                //     ClientSecrets = { new Secret(configuration.GetSection("TopologyAPI:clientSecret").Value.Sha256()) },
                //     // no interactive user, use the clientid/secret for authentication
                //     AllowedGrantTypes = GrantTypes.ClientCredentials,
                // 
                //     // scopes that client has access to
                //     AllowedScopes = configuration.GetSection("TopologyAPI:allowedScopes").Get<string[]>().ToList()
                // },
                #endregion

                #region Interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = configuration.GetSection("UserManager.APP:clientId").Value,
                    ClientName = configuration.GetSection("UserManager.APP:description").Value,
                    ClientSecrets = { new Secret(configuration.GetSection("UserManager.APP:clientSecret").Value.Sha256()) },

                    // where to redirect to after login
                    RedirectUris = { $"{configuration.GetSection("UserManager.APP:applicationUrl").Value}/signin-oidc" },

                    // where to redirect to after logout
                    FrontChannelLogoutUri = $"{configuration.GetSection("UserManager.APP:applicationUrl").Value}/signout-oidc",
                    PostLogoutRedirectUris = { $"{configuration.GetSection("UserManager.APP:applicationUrl").Value}/signout-callback-oidc" },

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                    AllowOfflineAccess = false,

                    AllowedScopes = userAllowedScopes
                },
                new Client
                {
                    ClientId = configuration.GetSection("Document.APP:clientId").Value,
                    ClientName = configuration.GetSection("Document.APP:description").Value,
                    ClientSecrets = { new Secret(configuration.GetSection("Document.APP:clientSecret").Value.Sha256()) },

                    // where to redirect to after login
                    RedirectUris = { $"{configuration.GetSection("Document.APP:applicationUrl").Value}/signin-oidc" },

                    // where to redirect to after logout
                    FrontChannelLogoutUri = $"{configuration.GetSection("Document.APP:applicationUrl").Value}/signout-oidc",
                    PostLogoutRedirectUris = { $"{configuration.GetSection("Document.APP:applicationUrl").Value}/signout-callback-oidc" },

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                    AllowOfflineAccess = false,

                    AllowedScopes = documentAllowedScopes
                },
                new Client
                {
                    ClientId = configuration.GetSection("Administrative.APP:clientId").Value,
                    ClientName = configuration.GetSection("Administrative.APP:description").Value,
                    ClientSecrets = { new Secret(configuration.GetSection("Administrative.APP:clientSecret").Value.Sha256()) },

                    // where to redirect to after login
                    RedirectUris = { $"{configuration.GetSection("Administrative.APP:applicationUrl").Value}/signin-oidc" },

                    // where to redirect to after logout
                    FrontChannelLogoutUri = $"{configuration.GetSection("Administrative.APP:applicationUrl").Value}/signout-oidc",
                    PostLogoutRedirectUris = { $"{configuration.GetSection("Administrative.APP:applicationUrl").Value}/signout-callback-oidc" },

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                    AllowOfflineAccess = false,

                    AllowedScopes = administrativeAllowedScopes
                },
                #endregion
            };
        }
    }
}

/*
 * Scope: profile 
 *      Climb: { name, given_name, family_name, middle_name, nickname, preferred_usename, ptofile, picture, website, gender, birthdate, zoneinfo, locale, updated_at }
 * Scope: email
 *      Climb: { email, email_verified }
 * Scope: address
 *      Climb: { address }
 * Scope: phone
 *      Climb: { phone_number, phone_number_verified }
 *      
 *      
 *      https://localhost:7193/signin-oidc => página inválida
 */