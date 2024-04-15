using Entity;
using Logic;

namespace Presentation.Models
{
    public class ProductController
    {
        public string FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Product.txt";
        public readonly Operator<Product> Operator;
        public ProductController()
        {
            this.Operator = new(FilePath);
        }
        public List<Product> FetchData()
        {
            return Operator.GetList();
        }
    }
}
