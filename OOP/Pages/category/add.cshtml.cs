using Logic.Validators;
using Presentation.Controllers;
using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.category
{
    public class AddModel : PageModel
    {
        private static readonly CategoryController _CategoryController = new();
        private readonly AccountController _AccountController = new();
        private readonly ItemValidator<Category> _Validator = new(_CategoryController.FilePath);
        public string? FeedBack;
        public string? Status;
        public void OnGet()
        {
            if (!_AccountController.ExistsUser(HttpContext.Session.GetString("username")))
            {
                Response.Redirect("/account/login");
            }
        }
        public void OnPost()
        {
            string name = Request.Form["name"].ToString();
            Category newC = new(name, 0);
            try
            {
                ServiceResult EndResult =_Validator.Add(newC);
                if(EndResult.IsSuccess())
                {
                    _CategoryController.HandleAdd(newC);
                    Response.Redirect("/view?i=ct");
                }
            }
            catch(Exception ex)  
            {
                FeedBack = ex.Message;
                Status = "is-invalid";
            }
        }
    }
}
