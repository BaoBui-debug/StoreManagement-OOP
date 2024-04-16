using Entity;
using Logic.ItemSeekers;
using Presentation.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages
{
    public class ViewModel : PageModel
    {
        private static ProductController _ProductController = new();
        private static CategoryController _CategoryController = new();
        public List<Product> Products = _ProductController.FetchData();
        public List<Category> Categories = _CategoryController.FetchData();
        private ProductFilter _ProductSeeker = new(_ProductController.FilePath);
        public string? id;
        public string? Entity;
        public string? FeedBack;
        public void OnGet()
        {
            id = Request.Query["i"].ToString();
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
        }
        public void OnPost()
        {
            id = Request.Query["i"].ToString();
            string request = Request.Form["search"].ToString();
            switch (id) 
            {
                case "pr":
                    Products = _ProductSeeker.FilterList(request);
                    FeedBack = Products.Count > 0 ? $"Tìm thấy {Products.Count} kết quả" : "Không tìm thấy kết quả nào";
                    break;
                case "ct":
                    Categories = _CategoryController.FetchData().FindAll(c => c.GetIdentifier() == request);
                    FeedBack = Categories.Count > 0 ? $"Tìm thấy {Categories.Count} kết quả" : "Không tìm thấy kết quả nào";
                    break;
            }
        }
    }
}
