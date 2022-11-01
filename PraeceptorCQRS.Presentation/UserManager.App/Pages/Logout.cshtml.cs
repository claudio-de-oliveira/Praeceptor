using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserManager.APP.Pages
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

            // var applicationUrl = _configuration.GetSection("Administrative.APP:applicationUrl").Value;
            // var redirectUri = $"{applicationUrl}/{_configuration.GetSection("Administrative.APP:logoutUri").Value}";

            return SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = "https://localhost:44388"
                },
                OpenIdConnectDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
