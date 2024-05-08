using DataAccess;
using Entity;

namespace Logic.Validators
{
    public class AccountValidator
    {
        private readonly ServiceResult _Result = new();
        private readonly Accessor<Account> _Accessor;
        public AccountValidator(string filePath)
        {
            this._Accessor = new(filePath);
        }
        public ServiceResult Verify(Account account) 
        {
            Account? valid = FindAccountMatches(account);
            if(valid == null)
            {
                _Result.AddError("Sai tên đăng nhập hoặc mật khẩu");
            }
            return _Result;
        }
        private Account? FindAccountMatches(Account account)
        {
            return _Accessor.Read().Find(acc => acc.Name.Equals(account.Name) && acc.Password.Equals(account.Password));
        }
    }
}
