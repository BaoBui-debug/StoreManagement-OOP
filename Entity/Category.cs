using Entity.Interface;

namespace Entity
{
    public class Category : IEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Category(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
        public bool IsDataComplete()
        {
            return !string.IsNullOrEmpty(Name);
        }
        public List<string> DataToStringList()
        {
            List<string> data = [Name, Quantity.ToString()];
            return data;
        }
        public string GetIdentifier()
        {
            return Name;
        }
    }
}
