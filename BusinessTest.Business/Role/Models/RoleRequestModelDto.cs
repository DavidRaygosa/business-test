namespace BusinessTest.Business.Role.Models
{
    public class RoleRequestModelDto
    {
        public string? Name { get; set; }
    }

    public class RoleUpdateRequestModelDto
    {
        public string? Name { get; set; }
        public List<string>? Permissions { get; set; }
    }
}