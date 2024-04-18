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
        private readonly ProductController _ProductController = new();
        public string CurrentName = string.Empty;
        public string? Note;
        public string? FeedBack;
        public string? Status;
        public void OnGet()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            Category Precursor = _Controller.FetchData()[index];
            CurrentName = Precursor.Name;
            Note = $"LƯU Ý: các sản phẩm thuộc phân loại {CurrentName} sẽ bị thay đổi";
        }
        public void OnPost() 
        {
            int index = int.Parse(Request.Query["id"].ToString());
            Category Precursor = _Controller.FetchData()[index];
            Category Successor = new(Request.Form["name"].ToString(), Precursor.Quantity);
            try
            {
                ServiceResult EndResult = _Validator.Update(Successor, index);
                if(EndResult.IsSuccess())
                {
                    _Controller.HandleUpdate(Successor, index);
                    _ProductController.OnCategoryModify(Precursor, Successor);
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
