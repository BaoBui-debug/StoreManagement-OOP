using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.account
{
    public class WelcomeModel : PageModel
    {
        private static readonly AccountController _AccountController = new();
        public string? UserName;
        public void OnGet()
        {
            if (_AccountController.ExistsUser(HttpContext.Session.GetString("username")))
            {
                UserName = HttpContext.Session.GetString("username");
            }
            else
            {
                Response.Redirect("/account/login");
            }
        }
    }
}
