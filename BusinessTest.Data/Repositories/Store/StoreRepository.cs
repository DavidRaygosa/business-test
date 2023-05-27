namespace BusinessTest.Data.Repositories.Store
{
    using BusinessTest.Data.BusinessDbContext;
    using BusinessTest.Entity.Store;

    public class StoreRepository : IStoreRepository
    {
        private readonly BusinessTestDbContext Context;
        public StoreRepository(BusinessTestDbContext context) => (Context) = (context);

        public List<Store>? GetAllByIds(List<long> Ids) => Context.Store?.Where(x => Ids.Contains(x.Id)).ToList();
        public List<Store>? GetAll() => Context.Store?.ToList();
        public void Update(Store Store) => Context.Store?.Update(Store);
        public Store? Get(long Id) => Context.Store?.Where(x => x.Id == Id).FirstOrDefault();
        public void Create(Store Store) => Context.Store?.Add(Store);
        public void Delete(Store Store) => Context.Store?.Remove(Store);
    }
}