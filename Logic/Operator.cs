using Logic.Interface;
using DataAccess;

namespace Logic
{
    public class Operator<T> : IOperator<T>
    {
        private readonly Accessor<T> _Accessor;
        public Operator(string filePath)
        {
            this._Accessor = new(filePath);
        }
        public void Add(T newItem)
        {
            List<T> list = _Accessor.Read();
            list.Add(newItem);
            _Accessor.Write(list);
        }
        public void Update(T updatedItem, int index)
        {
            List<T> list = _Accessor.Read();
            list[index] = updatedItem;
            _Accessor.Write(list);
        }
        public void Delete()
        {

        }
        public void Search(string id)
        {

        }
    }
}
