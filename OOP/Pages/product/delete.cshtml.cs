using Logic.ItemSeekers;
using Presentation.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.product
{
    public class DeleteModel : PageModel
    {
        private static readonly ProductController _Controller = new();
        private readonly ProductFilter _ItemSeeker = new(_Controller.FilePath);
        public string Message = "Dữ liệu về sản phẩm này sẽ bị xóa vĩnh viễn";
        public void OnPost()
        {
            string id = Request.Query["id"].ToString();
            _Controller.Operator.Delete(_ItemSeeker.GetIndex(id));
            Response.Redirect("/product/add");
        }
    }
}
