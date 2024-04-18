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
            int index = int.Parse(Request.Query["id"].ToString());
            Category target = _Controller.FetchData()[index];
            Message = $"Phân loại {target.Name} và các sản phẩm thuộc phân loại này sẽ bị xóa vĩnh viễn";
        }
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            Category target = _Controller.FetchData()[index];
            _Controller.HandleRemove(index);
            _ProductController.OnCategoryDelete(target);
            Response.Redirect("/view?i=ct");
        }
    }
}
