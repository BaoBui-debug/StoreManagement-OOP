using Entity;
using DataAccess;

namespace Logic.ItemSeekers
{
    public class ProductFilter
    {
        private readonly Accessor<Product> _Accessor;
        public ProductFilter(string filePath)
        {
            this._Accessor = new(filePath);
        }
        public Product LookForItem(string id)
        {
            Product? p = _Accessor.Read().Find(e => e.GetIdentifier() == id);
            return p ?? throw new Exception("Không tìm thấy sản phẩm");
        }
        public List<Product> FilterList(string request)
        {
            List<Product> resultA = _Accessor.Read().FindAll(e => e.GetIdentifier() == request);
            List<Product> resultB = _Accessor.Read().FindAll(e => e.Name.ToLower().Contains(request.ToLower()));
            return resultA.Count > 0 ? resultA : resultB;
        }
        public int GetIndex(string id)
        {
            return _Accessor.Read().FindIndex(e => e.GetIdentifier() == id);
        }
    }
}
