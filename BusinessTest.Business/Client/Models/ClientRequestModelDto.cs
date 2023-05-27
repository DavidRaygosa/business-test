namespace BusinessTest.Business.Client.Models
{
    public class CreateClientModelDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Direction { get; set; }
    }

    public class UpdateClientModelDto
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Direction { get; set; }
        public long? RoleId { get; set; }
    }
}