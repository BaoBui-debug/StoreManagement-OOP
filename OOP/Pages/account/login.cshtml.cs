using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.account
{
    public class loginModel : PageModel
    {
        public string? FeedBack;
        public void OnGet()
        {
            //lấy ra tài khoản đã đăng ký
        }
        public void OnPost() 
        {
            string Name = Request.Query["name"].ToString();
            string Password = Request.Query["password"].ToString();
            Account Login = new(Name, Password);
            try
            {

            }
            catch (Exception ex) 
            {
                FeedBack = ex.Message;
            }
        }
    }
}
