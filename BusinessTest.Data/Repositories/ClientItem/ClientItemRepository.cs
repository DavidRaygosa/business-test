namespace BusinessTest.Data.Repositories.ClientItem
{
    using BusinessTest.Data.BusinessDbContext;
    using BusinessTest.Entity.ClientItem;

    public class ClientItemRepository : IClientItemRepository
    {
        private readonly BusinessTestDbContext Context;
        public ClientItemRepository(BusinessTestDbContext context) => (Context) = (context);

        public void Delete(ClientItem ClientItem) => Context.ClientItem?.Remove(ClientItem);
        public void Update(ClientItem ClientItem) => Context.ClientItem?.Update(ClientItem);
        public ClientItem? Get(long Id) => Context.ClientItem?.Where(x => x.Id == Id).FirstOrDefault();
        public List<ClientItem>? GetAll(long? ItemId, long? ClientId)
        {
            if(ItemId is not null && ClientId is null)
                return Context.ClientItem?.Where(x => x.ItemId == ItemId).ToList();
            if(ItemId is null && ClientId is not null)
                return Context.ClientItem?.Where(x => x.ClientId == ClientId).ToList();
            if (ItemId is not null && ClientId is not null)
                return Context.ClientItem?.Where(x => x.ClientId == ClientId && x.ItemId == ItemId).ToList();

            return Context.ClientItem?.ToList();
        }
        public void Create(ClientItem ClientItem) => Context.ClientItem?.Add(ClientItem);
    }
}