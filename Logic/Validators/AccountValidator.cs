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
            if(!account.IsDataComplete())
            {
                _Result.AddError("Vui lòng điền đầy đủ tên và mật khẩu");
            }
            Account valid = _Accessor.Read()[0];
            if(account.Name != valid.Name)
            {
                _Result.AddError("Tên đăng nhập chưa đúng");
            }
            if(account.Password != valid.Password) 
            {
                _Result.AddError("Mật khẩu chưa đúng");
            }
            return _Result;
        }
    }
}
