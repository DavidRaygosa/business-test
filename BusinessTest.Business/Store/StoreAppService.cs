namespace BusinessTest.Business.Store
{
    using BusinessTest.Business.Store.Models;
    using BusinessTest.Data.Repositories;
    using BusinessTest.Data.Repositories.Store;
    using BusinessTest.Entity.Store;

    public class StoreAppService : BaseAppService, IStoreAppService
    {
        private readonly IStoreRepository StoreRepository;

        public StoreAppService(
            IUnitOfWork unitOfWork,
            IStoreRepository storeRepository
        ) : base(unitOfWork) => (StoreRepository) = (storeRepository);

        public Store? GetEntity(long Id)
        {
            if(Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            return StoreRepository.Get(Id);
        }

        public void Delete(long? Id)
        {
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Store? Store = StoreRepository.Get((long)Id);
            if (Store is null)
                throw new KeyNotFoundException("Store not found");

            // SAVE CHANGES
            StoreRepository.Delete(Store);
            UnitOfWork.SaveChanges();
        }

        public StoreResponseModelDto Get(long? Id)
        {
            if(Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Store? Store = StoreRepository.Get((long)Id);
            if (Store is null)
                throw new KeyNotFoundException("Store not found");

            StoreResponseModelDto StoreModel = new()
            {
                Id = Store.Id,
                Branch = Store.Branch,
                Direction = Store.Direction
            };
            return StoreModel;
        }

        public List<StoreResponseModelDto> GetAll()
        {
            List<StoreResponseModelDto> Stores = new();
            StoreRepository.GetAll()?.ForEach(Store => Stores.Add(new()
            {
                Id = Store.Id,
                Branch = Store.Branch,
                Direction = Store.Direction
            }));
            return Stores;
        }

        public void Update(long? Id, StoreRequestModelDto Body)
        {
            if(Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Store? Store = StoreRepository.Get((long)Id);
            if(Store is null)
                throw new KeyNotFoundException("Store not found");

            Store.Branch = Body.Branch is not null ? Body.Branch : Store.Branch;
            Store.Direction = Body.Direction is not null ? Body.Direction : Store.Direction;

            // SAVE CHANGES
            StoreRepository.Update(Store);
            UnitOfWork.SaveChanges();
        }

        public void Create(StoreRequestModelDto Body)
        {
            if(string.IsNullOrEmpty(Body.Branch))
                throw new HttpRequestException("Branch should not be empty or null");

            Store Store = new()
            {
                Branch = Body.Branch,
                Direction = Body.Direction is not null ? Body.Direction : null
            };

            // SAVE CHANGES
            StoreRepository.Create(Store);
            UnitOfWork.SaveChanges();
        }
    }
}