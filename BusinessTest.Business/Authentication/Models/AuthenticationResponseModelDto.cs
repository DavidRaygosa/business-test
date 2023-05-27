namespace BusinessTest.Business.Authentication.Models
{
    using BusinessTest.Business.Client.Models;

    public class AuthenticationResponseModelDto
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public ClientModelDto? Client { get; set; }
    }
}