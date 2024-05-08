using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.category
{
    public class DeleteModel : PageModel
    {
        private static readonly CategoryController _CategoryController = new();
        private readonly ProductController _ProductController = new();
        private static readonly AccountController _AccountController = new();
        public string? Message;
        public void OnGet()
        {
            if (!_AccountController.ExistsUser(HttpContext.Session.GetString("username")))
            {
                Response.Redirect("/account/login");
            }
            int index = int.Parse(Request.Query["id"].ToString());
            Category target = _CategoryController.FetchData()[index];
            Message = $"Phân loại {target.Name} và các sản phẩm thuộc phân loại này sẽ bị xóa vĩnh viễn";
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
