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
        private static readonly ProductController _ProductController = new();
        private readonly CategoryController _CategoryController = new();
        private readonly ImportController _ImportController = new();
        private readonly ProductValidator _Validator = new(_ProductController.FilePath);
        public List<Category> Categories = [];
        public List<Import> Imports = [];
        public string? Feedback;
        public string? Note;

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
            Product result = _ProductController.FetchData()[index];
            DefId = result.Id;
            DefName = result.Name;
            DefPrice = result.Price;
            DefQuantity = result.Category.Quantity;
            DefCategory = result.Category.Name;
            DefCompany = result.Company;
            DefMfg = result.Mfg;
            DefExp = result.Exp ?? DateOnly.MinValue;
            
            Categories = _CategoryController.FetchData();
            Imports = _ImportController.FetchData();
            Note = $"Thao tác này sẽ làm thay đổi trạng thái hóa đơn nhập hàng của sản phẩm này";
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
                    _ProductController.HandleUpdate(newP, index);
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
