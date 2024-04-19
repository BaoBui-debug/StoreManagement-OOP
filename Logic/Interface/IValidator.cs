using Entity.Interface;
using Logic.Validators;
using DataAccess;

namespace Logic.Interface
{
    public interface IValidator<T>
    {
        ServiceResult Result { get; set; }
        Accessor<T> Accessor { get; set; }
        ServiceResult Add(IEntity newItem);
    }
}
