using Entity;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages
{
    public class ViewModel : PageModel
    {
        private readonly ProductController _ProductController = new();
        private readonly CategoryController _CategoryController = new();
        public List<Product> Products = [];
        public List<Category> Categories = []; 
        public string? Entity;
        public string? FeedBack;
        public void OnGet()
        {
            string id = Request.Query["i"].ToString();
            switch (id)
            {
                case "pr":
                    Entity = "product";
                    break;
                case "ct":
                    Entity = "category";
                    break;
                case "ip":
                    Entity = "imports";
                    break;
                case "iv":
                    Entity = "invoices";
                    break;
            }
            Products = _ProductController.FetchData();
            Categories = _CategoryController.FetchData();
        }
        public void OnPost()
        {
            string request = Request.Form["search"].ToString();
            string id = Request.Query["i"].ToString();
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
