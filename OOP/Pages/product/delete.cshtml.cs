using Entity;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.product
{
    public class DeleteModel : PageModel
    {
        private readonly ProductController _ProductController = new();
        private static readonly AccountController _AccountController = new();
        public string Message = "Dữ liệu về sản phẩm này sẽ bị xóa vĩnh viễn";
        public void OnGet()
        {
            if (!_AccountController.ExistsUser(HttpContext.Session.GetString("username")))
            {
                Response.Redirect("/account/login");
            }
        }
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            _ProductController.HandleRemove(index);
            Response.Redirect("/view?i=pr");
        }
    }
}
