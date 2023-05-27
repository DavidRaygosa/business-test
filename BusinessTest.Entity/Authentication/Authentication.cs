namespace BusinessTest.Entity.Authentication
{
    using BusinessTest.Entity.Client;
    public class Authentication
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public long? ClientId { get; set; }
        public Client? Client { get; set; }
    }
}