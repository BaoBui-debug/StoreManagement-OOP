using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;
using Logic;

namespace Presentation.Pages.product
{
    public class DeleteModel : PageModel
    {
        private static readonly string _FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Product.txt";
        private readonly Operator<Product> _Operator = new(_FilePath);
        private readonly Seeker _ItemSeeker = new(_FilePath);
        public string Message = "Dữ liệu về sản phẩm này sẽ bị xóa vĩnh viễn";
        public void OnPost()
        {
            string id = Request.Query["id"].ToString();
            _ItemSeeker.LookForProduct(id);
            _Operator.Delete(_ItemSeeker.GetIndex());
            Response.Redirect("/product/add");
        }
    }
}
