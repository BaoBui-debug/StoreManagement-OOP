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
        public List<Product> LookFor(string request)
        {
            List<Product> resultA = _Operator.GetList().FindAll(e => e.GetIdentifier() == request);
            List<Product> resultB = _Operator.GetList().FindAll(e => e.Name.ToLower().Contains(request.ToLower()));
            return resultA.Count > 0 ? resultA : resultB;
        }
        public int GetIndex(string id)
        {
            return _Operator.GetList().FindIndex(e => e.GetIdentifier() == id);
        }
        public List<Product> Filter(int fromMonth, int toMonth, IEnumerable<int> priceRange, string companyName, string categoryName)
        {
            List<Product> source = _Operator.GetList();
            if(fromMonth != 1 && toMonth != 0)
            {
                source = source.FindAll(p => p.Mfg.Month >= fromMonth && p.Exp.HasValue && p.Exp.Value.Month <= toMonth);
            }
            if(toMonth == 0)
            {
                source = source.FindAll(p => p.Exp == null);
            }
            if(companyName != "")
            {
                source = source.FindAll(p => p.Company == companyName);
            }
            if(categoryName != "all")
            {
                source = source.FindAll(p => p.Category.Name == categoryName);
            }
            return source.FindAll(p => priceRange.Contains(p.Price));
        }
    }
}



