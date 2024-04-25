using Entity;
using Logic.Validators;
using Presentation.Controllers;
using Logic.TypeCheckers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.product
{
    public class EditModel : PageModel
    {
        private static readonly ProductController _ProductController = new();
        private static readonly ImportController _ImportController = new();
        private static readonly CategoryController _CategoryController = new();
        private static readonly ProductValidator _Validator = new(_ProductController.FilePath);
        public List<Category> Categories = _CategoryController.FetchData();
        public List<Import> Imports = _ImportController.FetchData();
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
            Note = $"Thao tác này sẽ làm thay đổi trạng thái hóa đơn nhập hàng của sản phẩm này";
        }
        public void OnPost()
        {
            string id = Request.Form["id"].ToString();
            string name = Request.Form["name"].ToString();
            string price = Request.Form["price"].ToString();
            string category = Request.Form["category"].ToString();
            string quantity = Request.Form["quantity"].ToString();
            string company = Request.Form["company"].ToString();
            string mfg = Request.Form["mfg"].ToString();
            string exp = Request.Form["exp"].ToString();
            Product? editP = ProductDataChecker.InputValidate(id, name, price, category, quantity, company, mfg, exp);
            if (editP == null)
            {
                Feedback = "Kiểu dữ liệu không đúng";
                return;
            }
            try
            {
                int index = int.Parse(Request.Query["id"].ToString());
                ServiceResult EndResult = _Validator.Update(editP, index);
                if (EndResult.IsSuccess())
                {
                    _ProductController.HandleUpdate(editP, index);
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
