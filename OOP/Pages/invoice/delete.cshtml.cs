using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.invoice
{
    public class DeleteModel : PageModel
    {
        private static readonly ImportController _ImportController = new();
        private static readonly InvoiceController _InvoiceController = new();
        public List<Import> Imports = _ImportController.FetchData();
        public string? Message;
        public void OnGet()
        {
            Message = "Hóa đơn này sẽ bị xóa vĩnh viễn";
        }
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            _InvoiceController.HandleRemove(index);
            Response.Redirect("/view?i=iv");
        }
    }
}
