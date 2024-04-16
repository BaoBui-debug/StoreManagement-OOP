using DataAccess;
using Entity;

namespace Logic.Validator
{
    public class CategoryValidator
    {
        private readonly ServiceResult _Result;
        private readonly Accessor<Category> _Accessor;
        public CategoryValidator(string filePath)
        {
            this._Accessor = new(filePath);
            this._Result = new();
        }
        public ServiceResult Add(Category newItem)
        {
            if (!newItem.IsDataComplete())
            {
                _Result.AddError("Không được phép thêm trường rỗng");
            }
            if (NameExisted(newItem))
            {
                _Result.AddError("Phân loại này đã tồn tại trong cơ sở dữ liệu");
            }
            return _Result;
        }
        public ServiceResult Update(Category successor, int index)
        {
            if (!successor.IsDataComplete())
            {
                _Result.AddError("Không được phép thêm trường rỗng");
            }
            if (SuccessorUnchanged(successor, index))
            {
                _Result.AddError("Thông tin không thay đổi, vui lòng cập nhật thông tin sản phẩm");
            }
            if (NameExisted(successor))
            {
                _Result.AddError("Phân loại này đã tồn tại trong cơ sở dữ liệu");
            }
            return _Result;
        }
        private bool NameExisted(Category newItem)
        {
            return _Accessor.Read().Exists(e => e.GetIdentifier() == newItem.GetIdentifier());
        }
        private bool SuccessorUnchanged(Category successor, int index)
        {
            Category precursor = _Accessor.Read()[index];
            return Helper.IsDataUnchanged(precursor, successor);
        }
    }
}
