using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.category
{
    public class DeleteModel : PageModel
    {
        private static readonly CategoryController _Controller = new();
        private readonly ProductController _ProductController = new();
        public string? Message;
        public void OnGet()
        {
            string id = Request.Query["id"].ToString();
            Category target = _Controller.HandleSearch(id)[0];
            Message = $"Phân loại {target.Name} và các sản phẩm thuộc phân loại này sẽ bị xóa vĩnh viễn";
        }
        public void OnPost()
        {
            string id = Request.Query["id"].ToString();
            Category target = _Controller.HandleSearch(id)[0];
            _Controller.HandleRemove(_Controller.GetIndex(id));
            _ProductController.OnCategoryDelete(target);
            Response.Redirect("/view?i=ct");
        }
    }
}
