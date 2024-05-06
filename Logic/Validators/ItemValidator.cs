using DataAccess;
using Entity.Interface;

namespace Logic.Validators
{
    public class ItemValidator<T>
    {
        private readonly ServiceResult _Result = new();
        private readonly Accessor<T> _Accessor;
        public ItemValidator(string filePath)
        {
            this._Accessor = new(filePath);
        }
        public ServiceResult Add(IEntity newItem)
        {
            if (!newItem.IsDataComplete())
            {
                _Result.AddError("Không được phép thêm trường rỗng");
            }
            if (IdExisted(newItem))
            {
                _Result.AddError("Mã vật phẩm đã tồn tại trong cơ sở dữ liệu");
            }
            return _Result;
        }
        public ServiceResult Update(IEntity successor, int index)
        {
            if (!successor.IsDataComplete())
            {
                _Result.AddError("Không được phép thêm trường rỗng");
            }
            if (SuccessorUnchanged(successor, index))
            {
                _Result.AddError("Thông tin không thay đổi, vui lòng cập nhật thông tin vật phẩm");
            }
            return _Result;
        }
        private bool IdExisted(IEntity newItem)
        {
            List<IEntity> entities = Helper.EntityCasting(_Accessor.Read());
            return entities.Exists(e => e.GetIdentifier() == newItem.GetIdentifier());
        }
        private bool SuccessorUnchanged(IEntity successor, int index)
        {
            List<IEntity> entities = Helper.EntityCasting(_Accessor.Read()); 
            IEntity precursor = entities[index];
            return Helper.IsDataUnchanged(precursor, successor);
        }
    }
}



