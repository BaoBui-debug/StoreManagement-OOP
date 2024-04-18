using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.product
{
    public class DeleteModel : PageModel
    {
        private readonly ProductController _Controller = new();
        public string Message = "Dữ liệu về sản phẩm này sẽ bị xóa vĩnh viễn";
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            _Controller.HandleRemove(index);
            Response.Redirect("/view?i=pr");
        }
    }
}
