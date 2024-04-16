using Entity;
using Logic.Validator;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.category
{
    public class EditModel : PageModel
    {
        private static readonly CategoryController _Controller = new();
        private readonly CategoryValidator _Validator = new(_Controller.FilePath);
        public string CurrentName = string.Empty;
        public string? FeedBack;
        public string? Status;
        public void OnGet()
        {
            string id = Request.Query["id"].ToString();
            Category Precursor = _Controller.HandleSearch(id)[0];
            CurrentName = Precursor.Name;
        }
        public void OnPost() 
        {
            string id = Request.Query["id"].ToString();
            int index = _Controller.GetIndex(id);
            Category Precursor = _Controller.HandleSearch(id)[0];
            Category Successor = new(Request.Form["name"].ToString(), Precursor.Quantity);
            try
            {
                ServiceResult EndResult = _Validator.Update(Successor, index);
                if(EndResult.IsSuccess())
                {
                    _Controller.HandleUpdate(Successor, index);
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
