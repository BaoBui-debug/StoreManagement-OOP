using Entity;
using Logic.Validators;
using Logic.TypeCheckers;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.product
{
    public class AddModel : PageModel
    {
        private static readonly ProductController _ProductController = new();
        private readonly ImportController _ImportController = new();
        private readonly CategoryController _CategoryController = new();
        private readonly ProductValidator _Validator = new(_ProductController.FilePath);
        public List<Category> Categories = [];
        public List<Import> Imports = [];
        public string? FeedBack;
        [BindProperty]
        public string Id { get; set; } = "";
        [BindProperty]
        public string Name { get; set; } = "";
        [BindProperty]
        public int Price { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public string Category { get; set; } = "";
        [BindProperty]
        public string Company { get; set; } = "";
        [BindProperty]
        public DateOnly Mfg { get; set; }
        [BindProperty]
        public DateOnly? Exp { get; set; }
        public void OnGet()
        {
            Categories = _CategoryController.FetchData();
            Imports = _ImportController.FetchData();
        }
        public void OnPost()
        {
            if (ProductDataChecker.IsInputInvalid(Price.ToString(), Quantity.ToString(), Mfg.ToString(), Exp.ToString()))
            {
                FeedBack = "Kiểu dữ liệu không đúng";
                return;
            }
            try
            {
                Category newC = new(Category, Quantity);
                Product newP = new(Id, Name, Price, newC, Company, Mfg, Exp);
                ServiceResult EndResult = _Validator.Add(newP);
                if (EndResult.IsSuccess())
                {
                    _ProductController.HandleAdd(newP);
                    Import newI = new(Id, Name, Price, Quantity);
                    _ImportController.HandleAdd(newI);
                    Response.Redirect("/view?i=pr");
                }
            }
            catch (Exception ex)
            {
                FeedBack = ex.Message;
            }
        }
    }
}
