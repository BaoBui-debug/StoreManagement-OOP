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
        public Product LookForProduct(string id)
        {
            Accessor<Product> list = new(_FilePath);
            Product? p = list.Read().Find(e => e.GetIdentifier() == id);
            _index = list.Read().FindIndex(e => e.GetIdentifier() == id);
            return p ?? throw new Exception("Không tìm thấy sản phẩm");
        }
        public int GetIndex()
        {
            return _index;
        }
    }
}
