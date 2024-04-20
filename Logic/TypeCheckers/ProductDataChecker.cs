using Entity;

namespace Logic.TypeCheckers
{
    public class ProductDataChecker
    {
        public static Product? InputValidate(string id, string name, string price, string category, string quantity, string company, string mfg, string exp)
        {
            if(int.TryParse(price, out int Price) && int.TryParse(quantity, out int Quantity) && DateOnly.TryParse(mfg, out DateOnly Mfg)) 
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
