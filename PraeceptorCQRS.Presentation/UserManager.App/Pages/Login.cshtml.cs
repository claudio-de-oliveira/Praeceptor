using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserManager.APP.Pages
{
    public class LoginModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string redirectUri)
        {
            // just to remove compiler warning
            await Task.CompletedTask;

            if (string.IsNullOrWhiteSpace(redirectUri))
            {
                redirectUri = Url.Content("~/holding/list");
            }
            // If user is already logged in, we can redirect directly...
            if (HttpContext.User.Identity is not null && HttpContext.User.Identity.IsAuthenticated)
            {
                var instituteClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "instituteid");

                if (instituteClaim is not null)
                    Response.Redirect($"{redirectUri}/{instituteClaim.Value}");
                else
                    Response.Redirect(redirectUri);
            }
            return Challenge(
                new AuthenticationProperties
                {
                    RedirectUri = redirectUri
                },
                OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
