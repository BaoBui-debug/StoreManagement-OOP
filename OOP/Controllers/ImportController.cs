using Entity;
using Logic.ItemSeekers;
using Logic;

namespace Presentation.Controllers
{
    public class ImportController
    {
        public string FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Import.txt";
        private readonly Operator<Import> _Operator;
        //private readonly ImportFilter _Filter;
        public ImportController()
        {
            this._Operator = new(FilePath);
            //this._Filter = new(_Operator);
        }
        /*
        public List<Product> HandleSearch(string request)
        {
            return _Filter.FilterList(request);
        }
         */
        public List<Import> FetchData()
        {
            return _Operator.GetList();
        }
        /*
        public int GetIndex(string request)
        {
            return _Filter.GetIndex(request);
        }
         */
        public void HandleAdd(Import i)
        {
            _Operator.Add(i);
        }
        public void HandleRemove(int index)
        {
            _Operator.Delete(index);
        }
        public void HandleUpdate(Import i, int index)
        {
            _Operator.Update(i, index);
        }
    }
}
