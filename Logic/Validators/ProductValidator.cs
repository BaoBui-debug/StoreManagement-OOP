using DataAccess;
using Entity;
using Entity.Interface;

namespace Logic.Validators
{
    public class ProductValidator
    {
        private readonly ServiceResult _Result;
        private readonly Accessor<Product> _Accessor;
        public ProductValidator(string filePath)
        {
            this._Accessor = new(filePath);
            this._Result = new();
        }
        public ServiceResult Add(Product newItem)
        {
            if (!newItem.IsDataComplete())
            {
                _Result.AddError("Không được phép thêm trường rỗng");
            }
            if (IdExisted(newItem))
            {
                _Result.AddError("Mã sản phẩm đã tồn tại trong cơ sở dữ liệu");
            }
            return _Result;
        }
        public ServiceResult Update(Product successor, int index)
        {
            if (!successor.IsDataComplete())
            {
                _Result.AddError("Không được phép thêm trường rỗng");
            }
            if (SuccessorUnchanged(successor, index))
            {
                _Result.AddError("Thông tin không thay đổi, vui lòng cập nhật thông tin sản phẩm");
            }
            return _Result;
        }
        private bool IdExisted(Product newItem)
        {
            return _Accessor.Read().Exists(e => e.GetIdentifier() == newItem.GetIdentifier());
        }
        private bool SuccessorUnchanged(Product successor, int index)
        {
            Product precursor = _Accessor.Read()[index];
            return Helper.IsDataUnchanged(precursor, successor);
        }
    }
}
