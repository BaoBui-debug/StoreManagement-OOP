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
            return _Filter.FilterList(request);
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
        public List<Product> GenerateOrder(string[] request)
        {
            List<Product> source = FetchData();
            List<Product> result = [];
            foreach (string item in request)
            {
                int index = source.FindIndex(p => p.Name == item.Split("/~/")[0]);
                if (index != -1)
                {
                    Product pReturn = source[index];
                    pReturn.Category.Quantity = int.Parse(item.Split("/~/")[1]);
                    result.Add(pReturn);
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
            foreach(Product p in successor)
            {
                Product? item = precursor.Find(i => i.GetIdentifier() == p.GetIdentifier());
                if(item != null)
                {
                    int oldQuantity = item.Category.Quantity;
                    int newQuantity = p.Category.Quantity;

                    if(oldQuantity < newQuantity)
                    {
                        item.Category.Quantity = newQuantity - oldQuantity; 
                        DecreaseQuantity(item);
                    }
                    if(oldQuantity > newQuantity)
                    {
                        item.Category.Quantity = oldQuantity - newQuantity;
                        IncreaseQuantity(item);
                    }
                }
                //nếu sản phẩm đã bị xóa --> trả số lượng của sản phẩm đó về kho
                else
                {
                    DecreaseQuantity(p);
                }
            }
        }
    }
}
