using Entity;

namespace Logic.ItemSeekers
{
    public class ImportFilter
    {
        private readonly Operator<Import> _Operator;
        public ImportFilter(Operator<Import> newOperator)
        {
            this._Operator = newOperator;
        }
        public List<Import> FilterList(string request)
        {
            List<Import> resultA = _Operator.GetList().FindAll(e => e.GetIdentifier() == request);
            List<Import> resultB = _Operator.GetList().FindAll(e => e.Name.ToLower().Contains(request.ToLower()));
            return resultA.Count > 0 ? resultA : resultB;
        }
        public int GetIndex(string id)
        {
            return _Operator.GetList().FindIndex(e => e.GetIdentifier() == id);
        }
    }
}
