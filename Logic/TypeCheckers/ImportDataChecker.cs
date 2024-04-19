namespace Logic.TypeCheckers
{
    public class ImportDataChecker
    {
        public static bool IsInputInvalid(string price, string quantity, string date)
        {
            if(!int.TryParse(price, out _) && !int.TryParse(quantity, out _) && !DateOnly.TryParse(date, out _))
            {
                return true;
            }
            return false;
        }
    }
}
