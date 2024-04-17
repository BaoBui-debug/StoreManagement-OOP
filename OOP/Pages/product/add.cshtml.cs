using Entity;
using Logic.Validator;
using Logic;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.product
{
    public class AddModel : PageModel
    {
        private static readonly ProductController _Controller = new();
        private readonly ProductValidator _Validator = new(_Controller.FilePath);
        public List<Category> categories = [];
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
            CategoryController CategoryController = new();
            categories = CategoryController.FetchData();
        }
        public void OnPost()
        {
            if (TypeChecker.IsInputInvalid(Price.ToString(), Quantity.ToString(), Mfg.ToString(), Exp.ToString()))
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
                    _Controller.HandleAdd(newP);
                    Import newI = new(Id, Name, Price, Quantity);
                    ImportController importController = new();
                    importController.HandleAdd(newI);
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
