using DataAccess;
using Entity;

namespace Logic.Validators
{
    public class InvoiceValidator
    {
        private readonly ServiceResult _Result;
        private readonly Accessor<Invoice> _Accessor;
        public InvoiceValidator(string filePath)
        {
            this._Accessor = new(filePath);
            this._Result = new();
        }
        public ServiceResult Add(Invoice newItem)
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
        public ServiceResult Update(Invoice successor, int index)
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
        private bool IdExisted(Invoice newItem)
        {
            return _Accessor.Read().Exists(e => e.GetIdentifier() == newItem.GetIdentifier());
        }
        private bool SuccessorUnchanged(Invoice successor, int index)
        {
            Invoice precursor = _Accessor.Read()[index];
            return Helper.IsDataUnchanged(precursor, successor);
        }
    }
}
