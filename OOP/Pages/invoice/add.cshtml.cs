using Entity;
using Logic.TypeCheckers;
using Logic.Validators;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.invoice
{
    public class AddModel : PageModel
    {
        private static readonly InvoiceController _InvoiceController = new();
        private static readonly ProductController _ProductController = new();
        private static readonly ImportController _ImportController = new();
        private static readonly InvoiceValidator _Validator = new(_InvoiceController.FilePath);
        public List<Product> Products = _ProductController.FetchData();
        public List<Import> Imports = _ImportController.FetchData();
        public string? FeedBack;
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            string id = Request.Form["id"].ToString();
            string customerName = Request.Form["name"].ToString();
            string date = Request.Form["date"].ToString();
            string selectedList = Request.Form["order"].ToString();
            List<Product> order = _ProductController.GenerateOrder(selectedList.Split("+#+"));
            Invoice? newIv = InvoiceDataChecker.InputValidate(id, customerName, order, date);
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
                    _ProductController.DecreaseQuantity(order);
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
