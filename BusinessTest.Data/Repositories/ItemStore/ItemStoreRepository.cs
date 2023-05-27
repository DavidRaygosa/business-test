namespace BusinessTest.Data.Repositories.ItemStore
{
    using BusinessTest.Data.BusinessDbContext;
    using BusinessTest.Entity.ItemStore;

    public class ItemStoreRepository : IItemStoreRepository
    {
        private readonly BusinessTestDbContext Context;
        public ItemStoreRepository(BusinessTestDbContext context) => (Context) = (context);

        public ItemStore? GetByRelationship(long ItemId, long StoreId) => Context.ItemStore?.Where(x => x.ItemId == ItemId && x.StoreId == StoreId).FirstOrDefault();
        public void Delete(ItemStore ItemStore) => Context.ItemStore?.Remove(ItemStore);
        public void Update(ItemStore ItemStore) => Context.ItemStore?.Update(ItemStore);
        public ItemStore? Get(long Id) => Context.ItemStore?.Where(x => x.Id == Id).FirstOrDefault();
        public List<ItemStore>? GetAll(long? ItemId, long? StoreId)
        {
            if(ItemId is not null && StoreId is null)
                return Context.ItemStore?.Where(x => x.ItemId == ItemId).ToList();
            if(ItemId is null && StoreId is not null)
                return Context.ItemStore?.Where(x => x.StoreId == StoreId).ToList();
            if(ItemId is not null && StoreId is not null)
                return Context.ItemStore?.Where(x => x.StoreId == StoreId && x.ItemId == ItemId).ToList();

            return Context.ItemStore?.ToList();
        }
        public void Create(ItemStore ItemStore) => Context.ItemStore?.Add(ItemStore);
    }
}