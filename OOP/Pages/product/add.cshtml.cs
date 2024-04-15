using Entity;
using Logic.Validator;
using Logic;
using Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.product
{
    public class AddModel : PageModel
    {
        private static ProductController _Controller = new();
        private readonly ProductValidator _Validator = new(_Controller.FilePath);
        public string? FeedBack;
        [BindProperty]
        public string Id { get; set; } = "";
        [BindProperty]
        public string Name { get; set; } = "";
        [BindProperty]
        public int Price { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public string Category { get; set; } = "";
        [BindProperty]
        public string Company { get; set; } = "";
        [BindProperty]
        public DateOnly Mfg { get; set; }
        [BindProperty]
        public DateOnly? Exp { get; set; }
        public void OnPost()
        {
            if (TypeChecker.IsInputInvalid(Price.ToString(), Quantity.ToString(), Mfg.ToString(), Exp.ToString()))
            {
                FeedBack = "Kiểu dữ liệu không đúng";
                return;
            }
            try
            {
                Category newC = new(Category, Quantity);
                Product newP = new(Id, Name, Price, newC, Company, Mfg, Exp);
                ServiceResult EndResult = _Validator.Add(newP);
                if (EndResult.IsSuccess())
                {
                    _Controller.Operator.Add(newP);
                }
            }
            catch (Exception ex)
            {
                FeedBack = ex.Message;
            }
        }
    }
}
