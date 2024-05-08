using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.account
{
    public class WelcomeModel : PageModel
    {
        public string? UserName;
        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") == "Admin")
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
