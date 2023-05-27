namespace BusinessTest.Business.Role.Models
{
    public class RoleResponseModelDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public List<string>? Permissions { get; set; }
    }
}