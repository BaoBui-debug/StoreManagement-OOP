using Entity.Interface;

namespace Entity
{
    public class Account : IEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Account(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }
        public bool IsDataComplete()
        {
            return Name != null && Password != null;
        }
        public List<string> DataToStringList()
        {
            List<string> data = [Name, Password];
            return data;
        }
        public string GetIdentifier()
        {
            return Name;
        }
    }
}
