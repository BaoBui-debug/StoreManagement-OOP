using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.invoice
{
    public class AddModel : PageModel
    {
        private static readonly ProductController _ProductController = new();
        private static readonly ImportController _ImportController = new();
        public List<Product> Products = _ProductController.FetchData();
        public List<Import> Imports = _ImportController.FetchData();
        public string? FeedBack;
        public void OnGet()
        {
        }
    }
}
