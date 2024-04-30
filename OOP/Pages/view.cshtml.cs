using Entity;
using Presentation.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Logic;

namespace Presentation.Pages
{
    public class ViewModel : PageModel
    {
        private static readonly ProductController _ProductController = new();
        private static readonly CategoryController _CategoryController = new();
        private static readonly ImportController _ImportController = new();
        private static readonly InvoiceController _InvoiceController = new();
        public List<Product> Products = _ProductController.FetchData();
        public List<Category> Categories = _CategoryController.FetchData();
        public List<Import> Imports = [];
        public List<Invoice> Invoices = [];
        public string? id;
        public string? Navigate;
        public string? FeedBack;
        public void OnGet()
        {
            id = Request.Query["i"].ToString();
            switch (id)
            {
                case "pr":
                    Navigate = "product";
                    Products = _ProductController.CheckItemLifeSpan();
                    break;
                case "ct":
                    Navigate = "category";
                    Categories = _CategoryController.SumUpAll(Products);
                    break;
                case "ip":
                    Navigate = "imports";
                    Imports = _ImportController.GetAccessibleItem(Products);
                    break;
                case "iv":
                    Navigate = "invoice";
                    Invoices = _InvoiceController.FetchData();
                    break;
            }
        }
        public void OnPost()
        {
            id = Request.Query["i"].ToString();
            switch (id)
            {
                case "pr":
                    Navigate = "product";
                    Products = _ProductController.CheckItemLifeSpan();
                    break;
                case "ct":
                    Navigate = "category";
                    Categories = _CategoryController.SumUpAll(Products);
                    break;
                case "ip":
                    Navigate = "imports";
                    Imports = _ImportController.GetAccessibleItem(Products);
                    break;
                case "iv":
                    Navigate = "invoice";
                    Invoices = _InvoiceController.FetchData();
                    break;
            }
            if (Request.Form.ContainsKey("search"))
            {
                id = Request.Query["i"].ToString();
                string request = Request.Form["search"].ToString();
                switch (id)
                {
                    case "pr":
                        Products = _ProductController.HandleSearch(request);
                        FeedBack = Products.Count > 0 ? $"Tìm thấy {Products.Count} kết quả" : "Không tìm thấy kết quả nào";
                        break;
                    case "ct":
                        Categories = _CategoryController.HandleSearch(request);
                        FeedBack = Categories.Count > 0 ? $"Tìm thấy {Categories.Count} kết quả" : "Không tìm thấy kết quả nào";
                        break;
                    case "ip":
                        Imports = _ImportController.HandleSearch(request);
                        FeedBack = Imports.Count > 0 ? $"Tìm thấy {Imports.Count} kết quả" : "Không tìm thấy kết quả nào";
                        break;
                    case "iv":
                        Invoices = _InvoiceController.HandleSearch(request);
                        FeedBack = Invoices.Count > 0 ? $"Tìm thấy {Invoices.Count} kết quả" : "Không tìm thấy kết quả nào";
                        break;
                }
            }
            else
            {
                id = Request.Query["i"].ToString();
                string mfgOption = Request.Form["mfg"].ToString();
                string expOption = Request.Form["exp"].ToString();
                string priceOption = Request.Form["price"].ToString();
                string companyName = Request.Form["company"].ToString();
                string categoryName = Request.Form["category"].ToString();
                IEnumerable<int> priceRange = Helper.GetRangeFromString(priceOption);
                switch(id) 
                {
                    case "pr":
                        Products = _ProductController.HandleFilter(int.Parse(mfgOption), int.Parse(expOption), priceRange, companyName, categoryName);
                        FeedBack = Products.Count > 0 ? $"Tìm thấy {Products.Count} kết quả" : "Không tìm thấy kết quả nào";
                        break;
                    case "ct":

                        break;
                    case "ip":

                        break;
                    case "iv":
                        break;
                }
            }
        }
    }
}
