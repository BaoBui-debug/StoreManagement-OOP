using Entity;

namespace Logic.ItemSeekers
{
    public class ProductFilter
    {
        private readonly Operator<Product> _Operator;
        public ProductFilter(Operator<Product> newOperator)
        {
            this._Operator = newOperator;
        }
        public List<Product> FilterList(string request)
        {
            List<Product> resultA = _Operator.GetList().FindAll(e => e.GetIdentifier() == request);
            List<Product> resultB = _Operator.GetList().FindAll(e => e.Name.ToLower().Contains(request.ToLower()));
            return resultA.Count > 0 ? resultA : resultB;
        }
        public int GetIndex(string id)
        {
            return _Operator.GetList().FindIndex(e => e.GetIdentifier() == id);
        }
        public List<Product> Filter(IEnumerable<int> mfgRange, IEnumerable<int>? expRange, IEnumerable<int> priceRange)
        {
            List<Product> source = _Operator.GetList();
            if(expRange == null)
            {
                return source.FindAll(p => mfgRange.Contains(p.Mfg.Month) && p.Exp == null && priceRange.Contains(p.Price));
            }
            return source.FindAll(p => mfgRange.Contains(p.Mfg.Month) && p.Exp.HasValue && expRange.Contains(p.Exp.Value.Month) && priceRange.Contains(p.Price));
        }
    }
}



