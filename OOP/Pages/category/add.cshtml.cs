using Logic.Validator;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;

namespace Presentation.Pages.category
{
    public class AddModel : PageModel
    {
        private static readonly CategoryController _Controller = new();
        private readonly CategoryValidator _Validator = new(_Controller.FilePath);
        public string? FeedBack;
        public string? Status;
        public void OnPost()
        {
            string name = Request.Form["name"].ToString();
            Category newC = new(name, 0);
            try
            {
                ServiceResult EndResult =_Validator.Add(newC);
                if(EndResult.IsSuccess())
                {
                    _Controller.HandleAdd(newC);
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
