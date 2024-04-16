using Entity;
using DataAccess;

namespace Logic
{
    public class Seeker
    {
        private readonly string _FilePath;
        private int _index;
        public Seeker(string filePath)
        {
            this._FilePath = filePath;
        }
        public Product LookForItem(string id)
        {
            Accessor<Product> list = new(_FilePath);
            Product? p = list.Read().Find(e => e.GetIdentifier() == id);
            _index = list.Read().FindIndex(e => e.GetIdentifier() == id);
            return p ?? throw new Exception("Không tìm thấy sản phẩm");
        }
        public List<Product> FilterList(string request)
        {
            Accessor<Product> list = new(_FilePath);
            List<Product> resultA = list.Read().FindAll(e => e.GetIdentifier() == request);
            List<Product> resultB = list.Read().FindAll(e => e.Name.ToLower().Contains(request.ToLower()));
            return resultA.Count > 0 ? resultA : resultB;
        }
        public int GetIndex(string id)
        {
            Accessor<Product> list = new(_FilePath);
            return list.Read().FindIndex(e => e.GetIdentifier() == id);
        }
    }
}
