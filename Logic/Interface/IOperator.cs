namespace Logic.Interface
{
    public interface IOperator<T>
    {
        void Add(T newItem);
        void Update(T updatedItem, int index);
        void Delete(int index);
        List<T> GetList();
    }
}
