using Entity;
using Logic.TypeCheckers;
using Logic.Validators;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.import
{
    public class EditModel : PageModel
    {
        private static readonly ImportController _ImportController = new();
        private static readonly ItemValidator<Import> _Validator = new(_ImportController.FilePath);
        public List<Import> Imports = _ImportController.FetchData();
        public string? FeedBack;
        public string? Note;

        public string DefId = "";
        public string DefName = "";
        public int DefPrice;
        public int DefQuantity;
        public DateOnly DefDate;
        public int DefTotal;
        public void OnGet()
        {
            if (HttpContext.Session.GetString("username") != "Admin")
            {
                Response.Redirect("/account/login");
            }
            int index = int.Parse(Request.Query["id"].ToString());
            Import I = _ImportController.FetchData()[index];
            DefId = I.Id;
            DefName = I.Name;
            DefPrice = I.Price;
            DefQuantity = I.Quantity;
            DefDate = I.Date;
            DefTotal = I.Total;
            Note = "LƯU Ý: thay đổi mã hóa đơn có thể dẫn đến hóa đơn rơi vào trạng thái đã hết hạn";
        }
        public void OnPost() 
        {
            string Id = Request.Form["id"].ToString();
            string Name = Request.Form["name"].ToString();
            string Price = Request.Form["price"].ToString();
            string Quantity = Request.Form["quantity"].ToString();
            string Date = Request.Form["date"].ToString();
            Import? Successor = DataChecker.ImportInputs(Id, Name, Price, Quantity);
            if(Successor == null)
            {
                FeedBack = "Kiểu dữ liệu chưa đúng";
                return;
            }   
            try
            {
                int index = int.Parse(Request.Query["id"].ToString());
                ServiceResult EndResult = _Validator.Update(Successor, index);
                if(EndResult.IsSuccess())
                {
                    _ImportController.HandleUpdate(Successor, index);
                    Response.Redirect("/view?i=ip");
                }
            }
            catch(Exception ex) 
            {
                FeedBack = ex.Message;
            }
        }
    }
}
