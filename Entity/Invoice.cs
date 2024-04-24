using Entity.Interface;

namespace Entity
{
    public class Invoice : IEntity
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public List<Product> Order { get; set; }
        public DateOnly Date {  get; set; }
        public int Total { get;}
        public Invoice(string id, string customerName, List<Product> order)
        {
            this.Id = id;
            this.CustomerName = customerName;
            this.Order = order;
            this.Date = DateOnly.FromDateTime(DateTime.Now);
            this.Total = order.Sum(p => p.Category.Quantity * p.Price);
        }
        public bool FieldsOccupied()
        {
            return Id != null && CustomerName != null && Order.Count > 0;
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
        public List<string> DataToStringList()
        {
            List<string> data = [Id, CustomerName, OrderToString(), Date.ToString(), Total.ToString()];
            return data;
        }
        public string GetIdentifier()
        {
            return Id;
        }
        private string OrderToString()
        {
            List<string> orders = [];
            foreach(Product p in Order)
            {
                List<string> data = p.DataToStringList();
                orders.Add(string.Join(",", data));
            }
            return string.Join(",", orders);
        }
    }
}