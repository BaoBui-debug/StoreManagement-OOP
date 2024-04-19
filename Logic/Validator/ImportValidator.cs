using DataAccess;
using Entity;

namespace Logic.Validator
{
    public class ImportValidator
    {
        private readonly ServiceResult _Result;
        private readonly Accessor<Import> _Accessor;
        public ImportValidator(string filePath)
        {
            this._Accessor = new(filePath);
            this._Result = new();
        }
        public ServiceResult Add(Import newItem)
        {
            if (!newItem.IsDataComplete())
            {
                _Result.AddError("Không được phép thêm trường rỗng");
            }
            if (IdExisted(newItem))
            {
                _Result.AddError("Mã hóa đơn đã tồn tại trong cơ sở dữ liệu");
            }
            return _Result;
        }
        public ServiceResult Update(Import successor, int index)
        {
            if (!successor.IsDataComplete())
            {
                _Result.AddError("Không được phép thêm trường rỗng");
            }
            if (SuccessorUnchanged(successor, index))
            {
                _Result.AddError("Thông tin không thay đổi, vui lòng cập nhật thông tin hóa đơn");
            }
            return _Result;
        }
        private bool IdExisted(Import newItem)
        {
            return _Accessor.Read().Exists(e => e.GetIdentifier() == newItem.GetIdentifier());
        }
        private bool SuccessorUnchanged(Import successor, int index)
        {
            Import precursor = _Accessor.Read()[index];
            return Helper.IsDataUnchanged(precursor, successor);
        }
    }
}
