using Entity;

namespace Logic.TypeCheckers
{
    public class InvoiceDataChecker
    {
        public static Invoice? InputValidate(string id, string customerName, List<Product> order, string date)
        {
            if(DateOnly.TryParse(date, out _))
            {
                Invoice validIv = new(id, customerName, order);
                return validIv;
            }
            return null;
        } 
    }
}
