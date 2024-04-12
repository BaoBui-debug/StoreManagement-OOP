namespace DataAccess.Interface
{
    public interface IAccessor<T>
    {
        List<T> Read();
        List<T> Expand(T newItem);
        void Write(List<T> newItem);
    }
}
