namespace BusinessTest.Data.Repositories.Client
{
    using BusinessTest.Entity.Client;
    public interface IClientRepository
    {
        void Delete(Client Client);
        void Update(Client Client);
        List<Client>? GetAll();
        Client? Get(long Id);
        void Create(Client Client);
    }
}