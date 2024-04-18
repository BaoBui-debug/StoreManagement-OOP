using Entity;
using Logic.Validator;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.product
{
    public class EditModel : PageModel
    {
        private static readonly ProductController _Controller = new();
        private readonly ProductValidator _Validator = new(_Controller.FilePath);
        public List<Category> categories = [];
        public string? Feedback;

        public string DefId = "";
        public string DefName = "";
        public int DefPrice;
        public int DefQuantity;
        public string DefCategory = "";
        public string DefCompany = "";
        public DateOnly DefMfg;
        public DateOnly DefExp;

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
            int index = int.Parse(Request.Query["id"].ToString());
            Product result = _Controller.FetchData()[index];
            DefId = result.Id;
            DefName = result.Name;
            DefPrice = result.Price;
            DefQuantity = result.Category.Quantity;
            DefCategory = result.Category.Name;
            DefCompany = result.Company;
            DefMfg = result.Mfg;
            DefExp = result.Exp ?? DateOnly.MinValue;
            
            CategoryController CategoryController = new();
            categories = CategoryController.FetchData();
        }
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            if (TypeChecker.IsInputInvalid(Price.ToString(), Quantity.ToString(), Mfg.ToString(), Exp.ToString()))
            {
                Feedback = "Kiểu dữ liệu không đúng";
                return;
            }
            Category newC = new(Category, Quantity);
            Product newP = new(Id, Name, Price, newC, Company, Mfg, Exp);
            try
            {
                ServiceResult EndResult = _Validator.Update(newP, index);
                if (EndResult.IsSuccess())
                {
                    _Controller.HandleUpdate(newP, index);
                    Response.Redirect("/view?i=pr");
                }
            }
            catch (Exception ex)
            {
                Feedback = ex.Message;
            }
        }
    }
}
