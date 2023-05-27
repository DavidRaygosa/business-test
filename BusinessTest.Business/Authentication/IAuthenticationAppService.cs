namespace BusinessTest.Business.Authentication
{
    using BusinessTest.Business.Authentication.Models;
    using BusinessTest.Entity.Authentication;
    using BusinessTest.Entity.Client;

    public interface IAuthenticationAppService
    {
        AuthenticationResponseModelDto? GetByUsername(string Username);
        void Create(Authentication Authentication);
        void Delete(Client Client);
        string Login(AuthenticationLoginRequestModelDto Body);
        List<string> Validate(string? Token);
    }
}