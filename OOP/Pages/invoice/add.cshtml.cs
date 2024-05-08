using Entity;
using Logic.TypeCheckers;
using Logic.Validators;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.invoice
{
    public class AddModel : PageModel
    {
        private static readonly InvoiceController _InvoiceController = new();
        private static readonly ProductController _ProductController = new();
        private static readonly ImportController _ImportController = new();
        private static readonly ItemValidator<Invoice> _Validator = new(_InvoiceController.FilePath);
        public List<Product> Products = _ProductController.FetchData();
        public List<Import> Imports = _ImportController.FetchData();
        public string? FeedBack;
        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") != "Admin")
            {
                Response.Redirect("/account/login");
            }
        }
        public void OnPost() 
        {
            string id = Request.Form["id"].ToString();
            string customerName = Request.Form["name"].ToString();
            string date = Request.Form["date"].ToString();
            string selectedList = Request.Form["order"].ToString();
            List<Product> order = _ProductController.GenerateOrder(selectedList.Split("+#+"));
            Invoice? newIv = DataChecker.InvoiceInputs(id, customerName, order, date);
            if(newIv == null)
            {
                FeedBack = "Kiểu dữ liệu chưa đúng";
                return;
            }
            try
            {
                ServiceResult EndResult = _Validator.Add(newIv);
                if(EndResult.IsSuccess())
                {
                    _InvoiceController.HandleAdd(newIv);
                    
                    foreach(Product p in order)
                    {
                        _ProductController.DecreaseQuantity(p);
                    } 
                    Response.Redirect("/view?i=iv");
                }
            }
            catch(Exception ex) 
            {
                FeedBack = ex.Message;
            }
        }    
    }
}
