using Entity.Interface;

namespace Entity
{
    public class Import : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateOnly Date {  get; set; }
        public int Total { get; }
        public bool Alive { get; set; }
        public Import(string id, string name, int price, int quantity)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
            this.Date = DateOnly.FromDateTime(DateTime.Now);
            this.Total = Price * Quantity;
            this.Alive = true;
        }
        public bool FieldsOccupied()
        {
            return Id != null && Name != null && Price != 0 && Quantity > 0;
        }
        public bool DateValid()
        {
            if(Date > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception($"Ngày thêm không được sau {DateOnly.FromDateTime(DateTime.Now)}");
            }
            return true;
        }
        public bool IsDataComplete()
        {
            return FieldsOccupied() && DateValid();
        }
        public static List<Import> StringToInvoiceList(string str)
        {
            string[] strI = str.Split(',');
            List<Import> result = [];
            foreach (string s in strI)
            {
                string[] ivs = s.Split(",");
                for (int i = 0; i < ivs.Length; i++)
                {
                    Import converted = new(ivs[0], ivs[1], int.Parse(ivs[2]), int.Parse(ivs[3]));
                    result.Add(converted);
                }
            }
            return result;
        }
        public List<string> DataToStringList()
        {
            List<string> data = [Id, Name, Price.ToString(), Quantity.ToString(), Date.ToString(), Total.ToString(), Alive.ToString()];
            return data;
        }
        public string GetIdentifier()
        {
            return Id;
        }
    }
}
