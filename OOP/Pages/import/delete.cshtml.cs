using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entity;

namespace Presentation.Pages.import
{
    public class DeleteModel : PageModel
    {
        private readonly ImportController _ImportController = new();
        private readonly AccountController _AccountController = new();
        public List<Import> Imports = [];
        public string Message = "Hóa đơn này sẽ bị xóa vĩnh viễn";
        public void OnGet()
        {
            if (!_AccountController.ExistsUser(HttpContext.Session.GetString("username")))
            {
                Response.Redirect("/account/login");
            }
            Imports = _ImportController.FetchData();
        }
        public void OnPost()
        {
            int index = int.Parse(Request.Query["id"].ToString());
            _ImportController.HandleRemove(index);
            Response.Redirect("/view?i=ip");
        }
    }
}
