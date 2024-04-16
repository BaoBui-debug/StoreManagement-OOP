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
    }
}
