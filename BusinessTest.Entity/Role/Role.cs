namespace BusinessTest.Entity.Role
{
    using BusinessTest.Entity.Client;

    public class Role
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Permissions { get; set; }
        public ICollection<Client>? Clients { get; set; }
    }
}