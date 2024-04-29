using Entity.Interface;

namespace Entity
{
    public class Product : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }
        public string Company { get; set; }
        public DateOnly Mfg { get; set; }
        public DateOnly? Exp { get; set; }
        public bool Dated { get; set; }
        public Product(string id, string name, int price, Category category, string company, DateOnly mfg, DateOnly? exp)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
            Company = company;
            Mfg = mfg;
            Exp = exp;
        }
        public bool IsDated()
        {
            return Exp <= DateOnly.FromDateTime(DateTime.Now);
        }
        public bool FieldsOccupied()
        {
            return Id != null && Name != null && Price != 0 && Category.Name != null && Category.Quantity > 0 && Company != null;
        }
        public bool DateValid()
        {
            if (Mfg > Exp)
            {
                throw new Exception("Ngày sản xuất phải sớm hơn ngày hết hạng");
            }
            if (Exp.HasValue && Exp <= DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception("Không thể thêm sản phẩm đã hết hạng sử dụng");
            }
            return true;
        }
        public bool IsDataComplete()
        {
            return FieldsOccupied() && DateValid();
        }
        public List<string> DataToStringList()
        {
            List<string> data = [Id, Name, Price.ToString(), Category.Name, Category.Quantity.ToString(), Company, Mfg.ToString(), Exp.ToString() ?? "không có"];
            return data;
        }
        public string GetIdentifier()
        {
            return Id;
        }
    }
}
