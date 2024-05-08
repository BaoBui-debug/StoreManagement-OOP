using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.invoice
{
    public class DeleteModel : PageModel
    {
        private static readonly ImportController _ImportController = new();
        private static readonly InvoiceController _InvoiceController = new();
        private static readonly AccountController _AccountController = new();
        public List<Import> Imports = _ImportController.FetchData();
        public string? Message;
        public void OnGet()
        {
            if (!_AccountController.ExistsUser(HttpContext.Session.GetString("username")))
            {
                Response.Redirect("/account/login");
            }
            Message = "LƯU Ý: Hóa đơn này sẽ bị xóa vĩnh viễn cùng các sản phẩm đã chọn, bạn chỉ nên dùng tính năng xóa khi hóa đơn đã được thanh toán để tránh tình trạng mất hàng";
        }
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            _InvoiceController.HandleRemove(index);
            Response.Redirect("/view?i=iv");
        }
    }
}
