namespace BusinessTest.Business.ItemStore
{
    using BusinessTest.Business.ItemStore.Models;

    public interface IItemStoreAppService
    {
        void Delete(long? Id);
        void Update(long? Id, ItemStoreRequestModelDto Body);
        ItemStoreResponseModelDto Get(long? Id);
        List<ItemStoreResponseModelDto> GetAll(long? ItemId, long? StoreId);
        void Create(ItemStoreRequestModelDto Body);
    }
}