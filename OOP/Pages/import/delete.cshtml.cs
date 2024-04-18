using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.import
{
    public class DeleteModel : PageModel
    {
        private readonly ImportController _Controller = new();
        public string Message = "Hóa đơn này sẽ bị xóa vĩnh viễn";
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            _Controller.HandleRemove(index);
            Response.Redirect("/view?i=ip");
        }
    }
}
