namespace Logic.TypeCheckers
{
    public class ProductDataChecker
    {
        public static bool IsInputInvalid(string price, string quantity, string mfg, string? exp)
        {
            if(!int.TryParse(price, out _) && !int.TryParse(quantity, out _) && !DateOnly.TryParse(mfg, out _) && !DateOnly.TryParse(exp, out _))
            {
                return true;
            }
            return false;
        }
    }
}
