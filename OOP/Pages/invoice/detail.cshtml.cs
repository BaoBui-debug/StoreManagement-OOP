using Presentation.Controllers;
using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.invoice
{
    public class DetailModel : PageModel
    {
        private static readonly InvoiceController _InvoiceController = new();
        public string Id = "";
        public string CustomerName = "";
        public List<Product> Order = [];
        public DateOnly Date;
        public int Total;
        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") != "Admin")
            {
                Response.Redirect("/account/login");
            }
            int index = int.Parse(Request.Query["id"].ToString());
            Invoice Iv = _InvoiceController.FetchData()[index];
            Id = Iv.Id;
            CustomerName = Iv.CustomerName;
            Order = Iv.Order;
            Date = Iv.Date;
            Total = Iv.Total;
        }
    }
}
