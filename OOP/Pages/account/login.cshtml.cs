using Entity;
using Logic.Validators;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.account
{
    public class LoginModel : PageModel
    {
        private static readonly AccountController _Controller = new();
        private static readonly AccountValidator _Validator = new(_Controller.FilePath);
        public string? FeedBack;
        public string Message = "Bạn phải đăng nhập để sử dụng ứng dụng này";
        public void OnPost() 
        {
            string Name = Request.Form["name"].ToString();
            string Password = Request.Form["password"].ToString();
            try
            {
                Account ac = new(Name, Password);
                ServiceResult EndResult = _Validator.Verify(ac);
                if(EndResult.IsSuccess())
                {
                    HttpContext.Session.SetString("username", ac.Name);
                    Response.Redirect("/account/welcome");
                }
            }
            catch (Exception ex) 
            {
                FeedBack = ex.Message;
            }
        }
    }
}
