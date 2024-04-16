using Entity;

namespace Logic.ItemSeekers
{
    public class CategoryFilter
    {
        private readonly Operator<Category> _Operator;
        public CategoryFilter(Operator<Category> newOperator)
        {
            this._Operator = newOperator;
        }
        public Category LookForItem(string id)
        {
            Category? p = _Operator.GetList().Find(e => e.GetIdentifier() == id);
            return p ?? throw new Exception("Không tìm thấy phân loại sản phẩm");
        }
        public List<Category> FilterList(string request)
        {
            List<Category> resultA = _Operator.GetList().FindAll(e => e.GetIdentifier() == request);
            List<Category> resultB = _Operator.GetList().FindAll(e => e.Name.ToLower().Contains(request.ToLower()));
            return resultA.Count > 0 ? resultA : resultB;
        }
        public int GetIndex(string id)
        {
            return _Operator.GetList().FindIndex(e => e.GetIdentifier() == id);
        }
    }
}
