namespace Market.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LogIn { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsSeller { get; set; }
    }
}
