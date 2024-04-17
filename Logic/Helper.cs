using Entity;
using Entity.Interface;

namespace Logic
{
    public class Helper
    {
        public static bool IsDataUnchanged(IEntity p, IEntity s)
        {
            int destination = p.DataToStringList().Count;
            int equals = 0;
            foreach (string i in p.DataToStringList())
            {
                foreach (string j in s.DataToStringList())
                {
                    if (j == i)
                    {
                        equals += 1;
                    }
                }
            }
            return equals == destination;
        }
    }
}
