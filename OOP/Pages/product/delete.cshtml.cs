using Presentation.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.product
{
    public class DeleteModel : PageModel
    {
        private readonly ProductController _Controller = new();
        public string Message = "Dữ liệu về sản phẩm này sẽ bị xóa vĩnh viễn";
        public void OnPost()
        {
            string id = Request.Query["id"].ToString();
            _Controller.HandleRemove(_Controller.GetIndex(id));
            Response.Redirect("/view?i=pr");
        }
    }
}
