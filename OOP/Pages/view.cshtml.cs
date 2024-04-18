using Entity;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages
{
    public class ViewModel : PageModel
    {
        private readonly ProductController _ProductController = new();
        private readonly CategoryController _CategoryController = new();
        private readonly ImportController _ImportController = new();
        public List<Product> Products = [];
        public List<Category> Categories = [];
        public List<Import> Imports = [];
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
            Products = _ProductController.FetchData();
            Categories = _CategoryController.SumUpAll(Products);
            Imports = _ImportController.GetAccessibleItem(Products);
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
