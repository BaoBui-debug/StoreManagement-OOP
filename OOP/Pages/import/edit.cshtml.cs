using Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Presentation.Controllers;

namespace Presentation.Pages.import
{
    public class EditModel : PageModel
    {
        private readonly ImportController _Controller = new();
        public string? FeedBack;
        public string DefId = "";
        public string DefName = "";
        public int DefPrice;
        public int DefQuantity;
        public DateOnly DefDate;
        public int DefTotal;
        public void OnGet()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            Import I = _Controller.FetchData()[index];
            DefId = I.Id;
            DefName = I.Name;
            DefPrice = I.Price;
            DefQuantity = I.Quantity;
            DefDate = I.Date;
            DefTotal = I.Total;
        }
    }
}
