using Entity;
using Logic;

namespace Presentation.Models
{
    public class CategoryController
    {
        public string FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Category.txt";
        public readonly Operator<Category> Operator;
        public CategoryController()
        {
            this.Operator = new(FilePath);
        }
        public List<Category> FetchData()
        {
            return Operator.GetList();
        }
    }
}
