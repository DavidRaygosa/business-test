namespace BusinessTest.Data.Repositories.Client
{
    using BusinessTest.Data.BusinessDbContext;
    using BusinessTest.Entity.Client;

    public class ClientRepository : IClientRepository
    {
        private readonly BusinessTestDbContext Context;
        public ClientRepository(BusinessTestDbContext context) => (Context) = (context);

        public void Delete(Client Client) => Context.Client?.Remove(Client);
        public void Update(Client Client) => Context.Client?.Update(Client);
        public List<Client>? GetAll() => Context.Client?.ToList();
        public Client? Get(long Id) => Context.Client?.Where(x => x.Id == Id).FirstOrDefault();
        public void Create(Client Client) => Context.Client?.Add(Client);
    }
}