namespace BusinessTest.Entity.Client
{
    using BusinessTest.Entity.ClientItem;
    using BusinessTest.Entity.Role;
    using BusinessTest.Entity.Authentication;

    public class Client
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Direction { get; set; }
        public long? RoleId { get; set; }
        public Role? Role { get; set; }
        public Authentication? Authentication { get; set; }
        public ICollection<ClientItem>? ClientItem { get; set; }
    }
}