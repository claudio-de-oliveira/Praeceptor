using Ardalis.GuardClauses;

using IdentityModel;

using IdentityServer4.Models;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Identity;

using PraeceptorCQRS.Domain.Entities;

using System.Security.Claims;

namespace IdentityServer.Api.Services
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            ApplicationUser user = await _userManager.GetUserAsync(context.Subject);

            IList<Claim> claims = new List<Claim>();

            IList<string> roles = await _userManager.GetRolesAsync(user);
            foreach (string role in roles)
                claims.Add(new Claim(JwtClaimTypes.Role, role));

            Guard.Against.Null(user.HoldingId);
            Guard.Against.Null(user.InstituteId);
            Guard.Against.Null(user.CourseId);

            claims.Add(new Claim(JwtClaimTypes.Name, user.UserName));
            // claims.Add(new Claim(JwtClaimTypes.GivenName, user.GivenName));
            // claims.Add(new Claim(JwtClaimTypes.FamilyName, "FamilyName"));
            claims.Add(new Claim(JwtClaimTypes.Email, "Email"));
            claims.Add(new Claim("holdingid", user.HoldingId.ToString()));
            claims.Add(new Claim("instituteid", user.InstituteId.ToString()));
            claims.Add(new Claim("courseid", user.CourseId.ToString()));
            claims.Add(new Claim("gender", $"{user.Gender}"));

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null) && user.IsEnabled;
        }
    }
}
