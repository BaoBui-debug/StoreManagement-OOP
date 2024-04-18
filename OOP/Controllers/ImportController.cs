using Entity;
using Logic.ItemSeekers;
using Logic;

namespace Presentation.Controllers
{
    public class ImportController
    {
        public string FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Import.txt";
        private readonly Operator<Import> _Operator;
        private readonly ImportFilter _Filter;
        public ImportController()
        {
            this._Operator = new(FilePath);
            this._Filter = new(_Operator);
        }
        public List<Import> HandleSearch(string request)
        {
            return _Filter.FilterList(request);
        }
        public List<Import> FetchData()
        {
            return _Operator.GetList();
        }
        public int GetIndex(string request)
        {
            return _Filter.GetIndex(request);
        }
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
        public List<Import> GetAccessibleItem(List<Product> pList)
        {
            List<Import> iList = FetchData();
            foreach(Import i in iList)
            {
                Product? item = pList.Find(p => p.GetIdentifier() == i.GetIdentifier());
                i.Alive = item != null;
                HandleUpdate(i, iList.IndexOf(i));
            }
            return iList;
        }
    }
}
