using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.account
{
    public class LogoutModel : PageModel
    {
        private static readonly AccountController _AccountController = new();
        public bool IsNotLoggedin;
        public string? Message;
        public void OnGet()
        {
            if (!_AccountController.ExistsUser(HttpContext.Session.GetString("username")))
            {
                IsNotLoggedin = true;
                Message = "Bạn chưa đăng nhập vào hệ thống";
            }
            else
            {
                IsNotLoggedin = false;
                Message = "Đăng xuất khỏi hệ thống?";
            }
        }
        public void OnPost() 
        {
            HttpContext.Session.Remove("username");
            Response.Redirect("/index");
        }
    }
}
