using Entity;
using Logic.Validators;
using Presentation.Controllers;
using Logic.TypeCheckers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.product
{
    public class AddModel : PageModel
    {
        private static readonly ProductController _ProductController = new();
        private static readonly ImportController _ImportController = new();
        private static readonly CategoryController _CategoryController = new();
        private static readonly ItemValidator<Product> _Validator = new(_ProductController.FilePath);
        public List<Category> Categories = _CategoryController.FetchData();
        public List<Import> Imports = _ImportController.FetchData();
        public string? FeedBack;
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
            Product? newP = DataChecker.ProductInputs(id, name, price, category, quantity, company, mfg, exp);
            if(newP == null) 
            {
                FeedBack = "Kiểu dữ liệu chưa đúng";
                return;
            }
            try
            {
                ServiceResult EndResult = _Validator.Add(newP);
                if (EndResult.IsSuccess())
                {
                    _ProductController.HandleAdd(newP);
                    Import newI = new(id, name, newP.Price, newP.Category.Quantity);
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
