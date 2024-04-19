using Logic.Validator;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;

namespace Presentation.Pages.category
{
    public class AddModel : PageModel
    {
        private static readonly CategoryController _CategoryController = new();
        private readonly ImportController _ImportController = new();
        private readonly CategoryValidator _Validator = new(_CategoryController.FilePath);
        public List<Import> Imports = [];
        public string? FeedBack;
        public string? Status;
        public void OnGet()
        {
            Imports = _ImportController.FetchData();
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
