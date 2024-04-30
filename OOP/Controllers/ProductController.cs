using Entity;
using Logic;
using Logic.ItemSeekers;

namespace Presentation.Controllers
{
    public class ProductController
    {
        public string FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Product.txt";
        private readonly Operator<Product> _Operator;
        private readonly ProductFilter _Filter;
        public ProductController()
        {
            this._Operator = new(FilePath);
            this._Filter = new(_Operator);
        }
        public List<Product> HandleSearch(string request)
        {
            return _Filter.LookFor(request);
        }
        public List<Product> HandleFilter(int fromMonth, int toMonth, IEnumerable<int> priceRange, string companyName, string categoryName)
        {
            return _Filter.Filter(fromMonth, toMonth, priceRange, companyName, categoryName);
        }
        public List<Product> FetchData()
        {
            return _Operator.GetList();
        }
        public int GetIndex(string request)
        {
            return _Filter.GetIndex(request);
        }
        public void HandleAdd(Product p)
        {
            _Operator.Add(p);
        }
        public void HandleRemove(int index)
        {
            _Operator.Delete(index);
        }
        public void HandleUpdate(Product p, int index)
        {
            _Operator.Update(p, index);
        }
        public List<Product> CheckItemLifeSpan()
        {
            List<Product> source = FetchData();
            foreach(Product p in source) 
            {
                if(p.Exp != null) 
                { 
                    p.Dated = p.IsDated();
                    HandleUpdate(p, source.IndexOf(p));
                }
            }
            return source;
        }
        public List<Product> GenerateOrder(string[] request)
        {
            List<Product> result = [];
            foreach (string item in request)
            {
                Product? order = FetchData().Find(p => p.Name == item.Split("/~/")[0]);
                if (order != null)
                {
                    order.Category.Quantity = int.Parse(item.Split("/~/")[1]);
                    result.Add(order);
                }
            }
            return result;
        }
        public void OnCategoryModify(Category precursor, Category successor)
        {
            List<Product> prList = FetchData();
            foreach (Product pr in prList)
            {
                if (pr.Category.Name == precursor.Name)
                {
                    pr.Category.Name = successor.Name;
                    HandleUpdate(pr, prList.IndexOf(pr));
                }
            }
        }
        public void OnCategoryDelete(Category c)
        {
            List<Product> prList = FetchData();
            foreach (Product pr in prList)
            {
                if (pr.Category.Name == c.Name)
                {
                    HandleRemove(prList.IndexOf(pr));
                }
            }
        }
        public void DecreaseQuantity(Product item)
        {
            int index = FetchData().FindIndex(u => u.GetIdentifier() == item.GetIdentifier());
            Product pEx = FetchData()[index];
            pEx.Category.Quantity -= item.Category.Quantity;
            HandleUpdate(pEx, index);
        }
        public void IncreaseQuantity(Product item)
        {
            int index = FetchData().FindIndex(u => u.GetIdentifier() == item.GetIdentifier());
            Product pEx = FetchData()[index];
            pEx.Category.Quantity += item.Category.Quantity;
            HandleUpdate(pEx, index);
        }
        public void OnInvoiceModify(List<Product> precursor, List<Product> successor)
        {
            foreach(Product oldItem in precursor) 
            {
                Product? matched = successor.Find(newItem => newItem.GetIdentifier() == oldItem.GetIdentifier());
                if(matched != null)
                {
                    int oldQuantity = oldItem.Category.Quantity;
                    int newQuantity = matched.Category.Quantity;
                    if(oldQuantity < newQuantity)
                    {
                        oldItem.Category.Quantity = newQuantity - oldQuantity;
                        DecreaseQuantity(oldItem);
                    }
                    else 
                    {
                        oldItem.Category.Quantity = oldQuantity - newQuantity;
                        IncreaseQuantity(oldItem);
                    }
                }
                else
                {
                    IncreaseQuantity(oldItem);
                }
            }
            foreach(Product newItem in successor)
            {
                if(precursor.Exists(oldItem => oldItem.GetIdentifier() == newItem.GetIdentifier()) == false)
                {
                    DecreaseQuantity(newItem);
                }
            }
        }
    }
}
