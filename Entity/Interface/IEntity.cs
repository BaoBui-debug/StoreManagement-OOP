namespace Entity.Interface
{
    public interface IEntity
    {
        bool IsDataComplete();
        List<string> DataToStringList();
        string GetIdentifier();
    }
}
