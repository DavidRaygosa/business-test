namespace BusinessTest.Data.Repositories.Authentication
{
    using BusinessTest.Entity.Authentication;
    using BusinessTest.Entity.Client;

    public interface IAuthenticationRepository
    {
        Authentication? GetByUserName(string Username);
        Authentication? GetByClient(Client Client);
        void Create(Authentication Authentication);
        void Delete(Authentication Authentication);
    }
}