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
        public bool ExistsUser(string? name) 
        {
            if(name != null) 
            {
                return _Operator.GetList().Find(a => a.Name == name) != null;
            }
            return false;
        }
    }

}
