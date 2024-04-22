using Entity;
using Logic.ItemSeekers;
using Logic;

namespace Presentation.Controllers
{
    public class InvoiceController
    {
        public string FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Invoice.txt";
        private readonly Operator<Invoice> _Operator;
        private readonly InvoiceFilter _Filter;
        public InvoiceController()
        {
            this._Operator = new(FilePath);
            this._Filter = new(_Operator);
        }
        public List<Invoice> HandleSearch(string request)
        {
            return _Filter.FilterList(request);
        }
        public List<Invoice> FetchData()
        {
            return _Operator.GetList();
        }
        public int GetIndex(string request)
        {
            return _Filter.GetIndex(request);
        }
        public void HandleAdd(Invoice i)
        {
            _Operator.Add(i);
        }
        public void HandleRemove(int index)
        {
            _Operator.Delete(index);
        }
        public void HandleUpdate(Invoice i, int index)
        {
            _Operator.Update(i, index);
        }
    }
}
