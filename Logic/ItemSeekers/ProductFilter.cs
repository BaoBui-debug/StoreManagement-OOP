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
        public Product LookForItem(string id)
        {
            Product? p = _Operator.GetList().Find(e => e.GetIdentifier() == id);
            return p ?? throw new Exception("Không tìm thấy sản phẩm");
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
    }
}
