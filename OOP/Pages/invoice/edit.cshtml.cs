using Entity;
using Logic.TypeCheckers;
using Logic.Validators;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.invoice
{
    public class EditModel : PageModel
    {
        private static readonly InvoiceController _InvoiceController = new();
        private static readonly ProductController _ProductController = new();
        private static readonly ImportController _ImportController = new();
        private static readonly AccountController _AccountController = new();
        private static readonly ItemValidator<Invoice> _Validator = new(_InvoiceController.FilePath);
        public List<Product> Products = _ProductController.FetchData();
        public List<Import> Imports = _ImportController.FetchData();
        public string? FeedBack;

        public string DefId = "";
        public string DefCustomerName = "";
        public string DefOrder = "";
        public DateOnly DefDate;
        public void OnGet()
        {
            if (!_AccountController.ExistsUser(HttpContext.Session.GetString("username")))
            {
                Response.Redirect("/account/login");
            }
            int index = int.Parse(Request.Query["id"].ToString());
            Invoice I = _InvoiceController.FetchData()[index];
            Products = _InvoiceController.GetMaxThreshold(Products, I.Order);
            DefId = I.Id;
            DefCustomerName = I.CustomerName;
            DefOrder = I.OrderToString();
            DefDate = I.Date;
        }
        public void OnPost()
        {
            string id = Request.Form["id"].ToString();
            string customerName = Request.Form["name"].ToString();
            string date = Request.Form["date"].ToString();
            string selectedList = Request.Form["order"].ToString();
            List<Product> order = _ProductController.GenerateOrder(selectedList.Split("+#+"));
            Invoice? editIv = DataChecker.InvoiceInputs(id, customerName, order, date);
            if (editIv == null)
            {
                FeedBack = "Kiểu dữ liệu chưa đúng";
                return;
            }
            try
            {
                int index = int.Parse(Request.Query["id"].ToString());
                ServiceResult EndResult = _Validator.Update(editIv, index);
                if (EndResult.IsSuccess())
                {
                    Invoice Iv = _InvoiceController.FetchData()[index];
                    _ProductController.OnInvoiceModify(Iv.Order, editIv.Order);
                    _InvoiceController.HandleUpdate(editIv, index);
                    Response.Redirect("/view?i=iv");
                }
            }
            catch (Exception ex)
            {
                FeedBack = ex.Message;
            }
        }
    }
}
