using Entity;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.product
{
    public class DeleteModel : PageModel
    {
        private readonly ProductController _ProductController = new();
        private static readonly ImportController _ImportController = new();
        public string Message = "Dữ liệu về sản phẩm này sẽ bị xóa vĩnh viễn";
        public List<Import> Imports = _ImportController.FetchData();
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            _ProductController.HandleRemove(index);
            Response.Redirect("/view?i=pr");
        }
    }
}
