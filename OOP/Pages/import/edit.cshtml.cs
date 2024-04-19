using Entity;
using Logic.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.import
{
    public class EditModel : PageModel
    {
        private static readonly ImportController _ImportController = new();
        private readonly ImportValidator _Validator = new(_ImportController.FilePath);
        public List<Import> Imports = [];
        public string? FeedBack;
        public string? Note;
        public string DefId = "";
        public string DefName = "";
        public int DefPrice;
        public int DefQuantity;
        public DateOnly DefDate;
        public int DefTotal;
        [BindProperty]
        public string Id { get; set; } = string.Empty;
        [BindProperty]
        public string Name { get; set; } = string.Empty;
        [BindProperty]
        public int Price { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public DateOnly Date { get; set; }
        public void OnGet()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            Import I = _ImportController.FetchData()[index];
            DefId = I.Id;
            DefName = I.Name;
            DefPrice = I.Price;
            DefQuantity = I.Quantity;
            DefDate = I.Date;
            DefTotal = I.Total;
            Imports = _ImportController.FetchData();
            Note = "LƯU Ý: thay đổi mã hóa đơn có thể dẫn đến hóa đơn rơi vào trạng thái đã hết hạn";
        }
        public void OnPost() 
        {
            try
            {
                int index = int.Parse(Request.Query["id"].ToString());
                Import Successor = new(Id, Name, Price, Quantity);
                ServiceResult EndResult = _Validator.Update(Successor, index);
                if(EndResult.IsSuccess())
                {
                    _ImportController.HandleUpdate(Successor, index);
                }
            }
            catch(Exception ex) 
            {
                FeedBack = ex.Message;
            }
        }
    }
}
