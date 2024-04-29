using Entity;

namespace Logic.ItemSeekers
{
    public class InvoiceFilter
    {
        private readonly Operator<Invoice> _Operator;
        public InvoiceFilter(Operator<Invoice> newOperator)
        {
            this._Operator = newOperator;
        }
        public List<Invoice> FilterList(string request)
        {
            List<Invoice> resultA = _Operator.GetList().FindAll(e => e.GetIdentifier() == request);
            List<Invoice> resultB = _Operator.GetList().FindAll(e => e.CustomerName.ToLower().Contains(request.ToLower()));
            return resultA.Count > 0 ? resultA : resultB;
        }
        public int GetIndex(string id)
        {
            return _Operator.GetList().FindIndex(e => e.GetIdentifier() == id);
        }
    }
}
