using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Models;

namespace Presentation.Pages
{
    public class ViewModel : PageModel
    {
        private static ProductController _ProductController = new();
        private static CategoryController _CategoryController = new();
        public List<Product> Products = _ProductController.FetchData();
        public List<Category> Categories = _CategoryController.FetchData();
        public string? id;
        public void OnGet()
        {
            id = Request.Query["i"].ToString();
        }
    }
}
