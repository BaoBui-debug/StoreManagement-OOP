using Entity;
using Logic;

namespace Presentation.Controllers
{
    public class AccountController
    {
        public string FilePath = "C:\\Users\\ACER\\OneDrive\\Desktop\\Programming Stuff\\RAZOR\\Storage\\Account.txt";
        private readonly Operator<Account> _Operator;
        public AccountController()
        {
            this._Operator = new(FilePath);
        }
        public List<Account> FetchData()
        {
            return _Operator.GetList();
        }
    }

}
