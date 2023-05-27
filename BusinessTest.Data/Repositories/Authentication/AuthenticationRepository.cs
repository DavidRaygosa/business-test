namespace BusinessTest.Data.Repositories.Authentication
{
    using BusinessTest.Data.BusinessDbContext;
    using BusinessTest.Entity.Authentication;
    using BusinessTest.Entity.Client;

    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly BusinessTestDbContext Context;
        public AuthenticationRepository(BusinessTestDbContext context) => (Context) = (context);

        public void Delete(Authentication Authentication) => Context.Authentication?.Remove(Authentication);
        public Authentication? GetByClient(Client Client) => Context.Authentication?.Where(x => x.Client == Client).FirstOrDefault();
        public Authentication? GetByUserName(string Username) => Context.Authentication?.Where(x => x.Username == Username).FirstOrDefault();
        public void Create(Authentication Authentication) => Context.Authentication?.Add(Authentication);
    }
}