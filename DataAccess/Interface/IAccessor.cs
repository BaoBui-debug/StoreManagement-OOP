namespace DataAccess.Interface
{
    public interface IAccessor<T>
    {
        List<T> Read();
        void Write(List<T> newItem);
    }
}
