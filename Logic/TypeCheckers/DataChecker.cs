using Entity;

namespace Logic.TypeCheckers
{
    public class DataChecker
    {
        public static Import? ImportInputs(string id, string name, string price, string quantity)
        {
            if (int.TryParse(price, out int Price) && int.TryParse(quantity, out int Quantity))
            {
                Import validIp = new(id, name, Price, Quantity);
                return validIp;
            }
            return null;
        }
        public static Invoice? InvoiceInputs(string id, string customerName, List<Product> order, string date)
        {
            if (DateOnly.TryParse(date, out _))
            {
                Invoice validIv = new(id, customerName, order);
                return validIv;
            }
            return null;
        }
        public static Product? ProductInputs(string id, string name, string price, string category, string quantity, string company, string mfg, string exp)
        {
            if (int.TryParse(price, out int Price) && int.TryParse(quantity, out int Quantity) && DateOnly.TryParse(mfg, out DateOnly Mfg))
            {
                DateOnly? Exp = DateOnly.TryParse(exp, out DateOnly e) ? e : null;
                Category newC = new(category, Quantity);
                Product newP = new(id, name, Price, newC, company, Mfg, Exp);
                return newP;
            }
            return null;
        }
    }
}
