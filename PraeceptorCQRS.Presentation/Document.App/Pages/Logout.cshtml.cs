using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Document.APP.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public LogoutModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // just to remove compiler warning
            await Task.CompletedTask;

            var applicationUrl = _configuration.GetSection("Document.APP:applicationUrl").Value;
            var redirectUri = $"{applicationUrl}/{_configuration.GetSection("Document.APP:logoutUri").Value}";

            return SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = redirectUri
                },
                OpenIdConnectDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
