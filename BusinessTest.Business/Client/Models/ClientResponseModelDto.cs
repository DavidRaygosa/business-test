namespace BusinessTest.Business.Client.Models
{
    public class ClientModelDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Direction { get; set; }
        public long? RoleId { get; set; }
        public List<string>? Permissions { get; set; }
    }
}