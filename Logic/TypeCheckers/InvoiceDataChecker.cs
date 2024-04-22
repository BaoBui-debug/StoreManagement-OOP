using Entity;

namespace Logic.TypeCheckers
{
    public class InvoiceDataChecker
    {
        public static Invoice? InputValidate(string id, string customerName, string order, string date)
        {
            if(DateOnly.TryParse(date, out _))
            {
                Invoice validIv = new(id, customerName, Invoice.StringToOrder(order));
                return validIv;
            }
            return null;
        } 
    }
}
