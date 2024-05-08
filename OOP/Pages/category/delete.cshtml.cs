using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.category
{
    public class DeleteModel : PageModel
    {
        private static readonly CategoryController _CategoryController = new();
        private readonly ImportController _ImportController = new();
        private readonly ProductController _ProductController = new();
        public List<Import> Imports = [];
        public string? Message;
        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") != "Admin")
            {
                Response.Redirect("/account/login");
            }
            int index = int.Parse(Request.Query["id"].ToString());
            Category target = _CategoryController.FetchData()[index];
            Message = $"Phân loại {target.Name} và các sản phẩm thuộc phân loại này sẽ bị xóa vĩnh viễn";
            Imports = _ImportController.FetchData();
        }
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            Category target = _CategoryController.FetchData()[index];
            _CategoryController.HandleRemove(index);
            _ProductController.OnCategoryDelete(target);
            Response.Redirect("/view?i=ct");
        }
    }
}
