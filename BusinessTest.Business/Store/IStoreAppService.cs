namespace BusinessTest.Business.Store
{
    using BusinessTest.Business.Store.Models;
    using BusinessTest.Entity.Store;

    public interface IStoreAppService
    {
        void Delete(long? Id);
        Store? GetEntity(long Ids);
        StoreResponseModelDto Get(long? Id);
        List<StoreResponseModelDto> GetAll();
        void Create(StoreRequestModelDto Body);
        void Update(long? id, StoreRequestModelDto Body);
    }
}