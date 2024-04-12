using Entity;
using DataAccess;
namespace Logic.Validator
{
    public class ProductValidator
    {
        private ServiceResult Result { get; set; } = new();
        private Accessor<Product> Accessor { get; set; }
        public ProductValidator(string filePath)
        {
            this.Accessor = new(filePath);
        }
        public ServiceResult Add(Product newItem)
        {
            if (!newItem.IsDataComplete())
            {
                Result.AddError("Không được phép thêm trường rỗng");
            }
            if (IdExisted(newItem))
            {
                Result.AddError("Mã sản phẩm đã tồn tại trong cơ sở dữ liệu");
            }
            return Result;
        }
        public ServiceResult Update(Product successor, int index)
        {
            if (!successor.IsDataComplete())
            {
                Result.AddError("Không được phép thêm trường rỗng");
            }
            if (SuccessorUnchanged(successor, index))
            {
                Result.AddError("Thông tin không thay đổi, vui lòng cập nhật thông tin sản phẩm");
            }
            return Result;
        }
        private bool IdExisted(Product newItem)
        {
            return Accessor.Read().Exists(e => e.GetIdentifier() == newItem.GetIdentifier());
        }
        private bool SuccessorUnchanged(Product successor, int index)
        {
            Product precursor = Accessor.Read()[index];
            return Helper.IsDataUnchanged(precursor, successor);
        }
    }
    public class ServiceResult
    {
        private readonly List<string> _Unresolved = [];
        public void AddError(string error)
        {
            _Unresolved.Add(error);
        }
        public bool IsSuccess()
        {
            if (_Unresolved.Count > 0)
            {
                AutoExceptionThrow();
            }
            return _Unresolved.Count == 0;
        }
        private void AutoExceptionThrow()
        {
            throw new Exception(_Unresolved[0]);
        }
    }
}
