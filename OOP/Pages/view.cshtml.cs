using Entity;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages
{
    public class ViewModel : PageModel
    {
        private static readonly string _FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Product.txt";
        private readonly Operator<Product> _Operator = new(_FilePath);
        public List<Product> data = [];
        public void OnGet()
        {
            string item = Request.Query["i"].ToString();
            data = _Operator.GetList();

        }
    }
}
