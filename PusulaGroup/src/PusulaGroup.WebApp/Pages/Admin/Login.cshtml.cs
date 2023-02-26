using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace PusulaGroup.WebApp.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public LoginModel(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            ViewData["title"] = "Login | Admin";
        }

        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            if ("admin" == username && "Ghgh21021453…" == password)
            {
                await HttpContext.SignOutAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                SetSuccessMessage("Login successful. You are being redirected to the admin index page");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("/Admin/Index");
            }
            else
            {
                SetErrorMessage("Username or password wrong.");
            }
            return RedirectToPage("/Admin/Login");
        }

        private void SetErrorMessage(string errorMessage)
        {
            TempData["ErrorMessage"] = errorMessage;
        }

        private void SetSuccessMessage(string successMessage)
        {
            TempData["SuccessMessage"] = successMessage;
        }
    }
}
