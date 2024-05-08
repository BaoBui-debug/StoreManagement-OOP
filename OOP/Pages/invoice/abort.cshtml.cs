using Presentation.Controllers;
using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.invoice
{
    public class AbortModel : PageModel
    {
        private static readonly ImportController _ImportController = new();
        private static readonly InvoiceController _InvoiceController = new();
        private static readonly ProductController _ProductController = new();
        private static readonly AccountController _AccountController = new();
        public List<Import> Imports = _ImportController.FetchData();
        public string? Note;
        public void OnGet()
        {
            if (!_AccountController.ExistsUser(HttpContext.Session.GetString("username")))
            {
                Response.Redirect("/account/login");
            }
            Note = "LƯU Ý: Hóa đơn này sẽ bị hủy và các sản phẩm đã chọn sẽ được trả về kho";
        }
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            Invoice Iv = _InvoiceController.FetchData()[index];
            foreach(Product p in Iv.Order)
            {
                _ProductController.IncreaseQuantity(p);
            }
            _InvoiceController.HandleRemove(index);
            Response.Redirect("/view?i=iv");
        }
    }
}
