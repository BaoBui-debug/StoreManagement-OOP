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
            List<Import> result = _Operator.GetList().FindAll(e => e.GetIdentifier() == request);
            return result;
        }
        public int GetIndex(string id)
        {
            return _Operator.GetList().FindIndex(e => e.GetIdentifier() == id);
        }
    }
}
