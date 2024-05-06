using Entity;
using Logic.Validators;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.category
{
    public class EditModel : PageModel
    {
        private static readonly CategoryController _CategoryController = new();
        private readonly ProductController _ProductController = new();
        private readonly ImportController _ImportController = new();
        private readonly ItemValidator<Category> _Validator = new(_CategoryController.FilePath);
        public List<Import> Imports = [];
        public string CurrentName = string.Empty;
        public string? Note;
        public string? FeedBack;
        public string? Status;
        public void OnGet()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            Category Precursor = _CategoryController.FetchData()[index];
            CurrentName = Precursor.Name;
            Note = $"LƯU Ý: các sản phẩm thuộc phân loại {CurrentName} sẽ bị thay đổi";
            Imports = _ImportController.FetchData();
        }
        public void OnPost() 
        {
            int index = int.Parse(Request.Query["id"].ToString());
            Category Precursor = _CategoryController.FetchData()[index];
            Category Successor = new(Request.Form["name"].ToString(), Precursor.Quantity);
            try
            {
                ServiceResult EndResult = _Validator.Update(Successor, index);
                if(EndResult.IsSuccess())
                {
                    _CategoryController.HandleUpdate(Successor, index);
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
