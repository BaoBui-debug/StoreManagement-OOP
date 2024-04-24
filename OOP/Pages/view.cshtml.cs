using Entity;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages
{
    public class ViewModel : PageModel
    {
        private static readonly ProductController _ProductController = new();
        private static readonly CategoryController _CategoryController = new();
        private static readonly ImportController _ImportController = new();
        private static readonly InvoiceController _InvoiceController = new();
        public List<Product> Products = _ProductController.FetchData();
        public List<Category> Categories = _CategoryController.FetchData();
        public List<Import> Imports = _ImportController.FetchData();
        public List<Invoice> Invoices = _InvoiceController.FetchData();
        public string? id;
        public string? Navigate;
        public string? FeedBack;
        public void OnGet()
        {
            id = Request.Query["i"].ToString();
            switch (id)
            {
                case "pr":
                    Navigate = "product";
                    break;
                case "ct":
                    Navigate = "category";
                    break;
                case "ip":
                    Navigate = "imports";
                    break;
                case "iv":
                    Navigate = "invoices";
                    break;
            }
        }
        public void OnPost()
        {
            string request = Request.Form["search"].ToString();
            id = Request.Query["i"].ToString();
            switch (id) 
            {
                case "pr":
                    Products = _ProductController.HandleSearch(request);
                    FeedBack = Products.Count > 0 ? $"Tìm thấy {Products.Count} kết quả" : "Không tìm thấy kết quả nào";
                    break;
                case "ct":
                    Categories = _CategoryController.HandleSearch(request);
                    FeedBack = Categories.Count > 0 ? $"Tìm thấy {Categories.Count} kết quả" : "Không tìm thấy kết quả nào";
                    break;
            }
        }
    }
}
