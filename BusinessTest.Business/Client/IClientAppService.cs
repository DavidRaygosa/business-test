namespace BusinessTest.Business.Client
{
    using BusinessTest.Business.Client.Models;
    using BusinessTest.Entity.Client;

    public interface IClientAppService
    {
        void Delete(long? Id);
        void Update(long? Id, UpdateClientModelDto Body);
        List<ClientModelDto> GetAll();
        Client? GetEntity(long? Id);
        ClientModelDto GetOne(long? Id);
        void Create(CreateClientModelDto Body);
    }
}