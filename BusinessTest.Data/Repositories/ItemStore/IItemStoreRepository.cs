namespace BusinessTest.Data.Repositories.ItemStore
{
    using BusinessTest.Entity.ItemStore;

    public interface IItemStoreRepository
    {
        void Delete(ItemStore ItemStore);
        void Update(ItemStore ItemStore);
        ItemStore? Get(long Id);
        List<ItemStore>? GetAll(long? ItemId, long? StoreId);
        void Create(ItemStore ItemStore);
        ItemStore? GetByRelationship(long ItemId, long StoreId);
    }
}