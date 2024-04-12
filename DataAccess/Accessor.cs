using Newtonsoft.Json;
using DataAccess.Interface;

namespace DataAccess
{
    public class Accessor<T> : IAccessor<T>
    {
        private string FilePath { get; set; }
        public Accessor(string path)
        {
            this.FilePath = path;
        }
        public List<T> Read()
        {
            StreamReader reader = new(FilePath);
            string json = reader.ReadToEnd();
            reader.Close();
            return JsonConvert.DeserializeObject<List<T>>(json) ?? throw new Exception("Đã có lỗi xảy ra trong cơ sở dữ liệu");
        }
        public List<T> Expand(T newItem)
        {
            List<T> list = Read();
            list.Add(newItem);
            return list;
        }
        public void Write(List<T> newList)
        {
            StreamWriter writer = new(FilePath);
            string json = JsonConvert.SerializeObject(newList);
            writer.Write(json);
            writer.Close();
        }
    }
}
