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
        public string? Entity;
        public void OnGet()
        {
            /*
             pr : Product
            ct : Category
            ip : Imports
            iv : Invoices
            */
            id = Request.Query["i"].ToString();
            switch(id) 
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
    }
}
