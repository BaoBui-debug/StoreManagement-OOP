using Entity;
using Logic;
using Logic.ItemSeekers;

namespace Presentation.Controllers
{
    public class CategoryController
    {
        public string FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Category.txt";
        private readonly Operator<Category> _Operator;
        private readonly CategoryFilter _Filter;
        public CategoryController()
        {
            this._Operator = new(FilePath);
            this._Filter = new(_Operator);
        }
        public List<Category> HandleSearch(string request)
        {
            return _Filter.FilterList(request);
        } 
        public List<Category> FetchData()
        {
            return _Operator.GetList();
        }
        public int GetIndex(string request) 
        {
            return _Filter.GetIndex(request);
        }
        public void HandleAdd(Category newC)
        {
            _Operator.Add(newC);
        }
    }
}
