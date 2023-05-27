namespace BusinessTest.Data.Repositories.Store
{
    using BusinessTest.Entity.Store;

    public interface IStoreRepository
    {
        void Delete(Store Store);
        List<Store>? GetAll();
        List<Store>? GetAllByIds(List<long> Ids);
        void Update(Store Store);
        void Create(Store Store);
        Store? Get(long Id);
    }
}