using Entity;
using Logic;
namespace Controller
{
    public class ProductController
    {
        private static readonly string _FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Product.txt";
        private readonly Operator<Product> _Operator = new(_FilePath);
        public List<Product> FetchData()
        {
            return _Operator.GetList();
        }
    }

}
