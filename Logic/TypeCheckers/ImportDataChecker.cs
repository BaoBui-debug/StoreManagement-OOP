using Entity;

namespace Logic.TypeCheckers
{
    public class ImportDataChecker
    {
        public static Import? InputValidate(string id, string name, string price, string quantity)
        {
            if(int.TryParse(price, out int Price) && int.TryParse(quantity, out int Quantity))
            {
                Import validI = new(id, name, Price, Quantity);
                return validI;
            }
            return null;
        }
    }
}
