namespace BusinessTest.Data.Repositories.ClientItem
{
    using BusinessTest.Entity.ClientItem;

    public interface IClientItemRepository
    {
        void Delete(ClientItem ClientItem);
        void Update(ClientItem ClientItem);
        ClientItem? Get(long Id);
        List<ClientItem>? GetAll(long? ItemId, long? ClientId);
        void Create(ClientItem ClientItem);
    }
}