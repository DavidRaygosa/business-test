namespace BusinessTest.Business.ItemStore
{
    using BusinessTest.Business.Item;
    using BusinessTest.Entity.Item;
    using BusinessTest.Business.ItemStore.Models;
    using BusinessTest.Business.Store;
    using BusinessTest.Entity.Store;
    using BusinessTest.Entity.ItemStore;
    using BusinessTest.Data.Repositories.ItemStore;
    using BusinessTest.Data.Repositories;

    public class ItemStoreAppService : BaseAppService, IItemStoreAppService
    {
        private readonly IItemAppService ItemAppService;
        private readonly IStoreAppService StoreAppService;
        private readonly IItemStoreRepository ItemStoreRepository;

        public ItemStoreAppService(
            IUnitOfWork unitOfWork,
            IItemAppService itemAppService,
            IStoreAppService storeAppService,
            IItemStoreRepository itemStoreRepository
        ) : base(unitOfWork) => (ItemAppService, StoreAppService, ItemStoreRepository) = (itemAppService, storeAppService, itemStoreRepository);

        public void Delete(long? Id)
        {
            //
            if(Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            ItemStore? ItemStore = ItemStoreRepository.Get((long)Id);
            if(ItemStore is null)
                throw new KeyNotFoundException("ItemStore not found");

            // SAVE CHANGES
            ItemStoreRepository.Delete(ItemStore);
            UnitOfWork.SaveChanges();
        }

        public void Update(long? Id, ItemStoreRequestModelDto Body)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            ItemStore? ItemStore = ItemStoreRepository.Get((long)Id);
            if (ItemStore is null)
                throw new KeyNotFoundException("ItemStore not found");

            if(Body.ItemId > 0)
            {
                //
                Item? Item = ItemAppService.GetEntity(Body.ItemId);
                if (Item is null)
                    throw new KeyNotFoundException("Item not found");

                ItemStore.Item = Item;
            }

            if(Body.StoreId > 0)
            {
                //
                Store? Store = StoreAppService.GetEntity((long)Body.StoreId);
                if (Store is null)
                    throw new KeyNotFoundException("Stores not found");

                ItemStore.Store = Store;
            }

            // SAVE CHANGES
            ItemStoreRepository.Update(ItemStore);
            UnitOfWork.SaveChanges();
        }

        public ItemStoreResponseModelDto Get(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            ItemStore? ItemStore = ItemStoreRepository.Get((long)Id);
            if (ItemStore is null)
                throw new KeyNotFoundException("ItemStore not found");

            ItemStoreResponseModelDto ItemModel = new()
            {
                Id = ItemStore.Id,
                Item = ItemStore.ItemId > 0 ? ItemAppService.Get(ItemStore.ItemId) : null,
                Store = ItemStore.StoreId > 0 ? StoreAppService.Get(ItemStore.StoreId) : null,
                Date = ItemStore.Date
            };
            return ItemModel;
        }

        public List<ItemStoreResponseModelDto> GetAll(long? ItemId, long? StoreId)
        {
            List<ItemStoreResponseModelDto> ItemStores = new();
            ItemStoreRepository.GetAll(ItemId, StoreId)?.ForEach(ItemStore =>
            {
                ItemStores.Add(new()
                {
                    Id = ItemStore.Id,
                    Item = ItemStore.ItemId > 0 ? ItemAppService.Get(ItemStore.ItemId) : null,
                    Store = ItemStore.StoreId > 0 ? StoreAppService.Get(ItemStore.StoreId) : null,
                    Date = ItemStore.Date
                });
            });
            return ItemStores;
        }

        public void Create(ItemStoreRequestModelDto Body)
        {
            //
            if(Body.ItemId is null || Body.ItemId == 0)
                throw new HttpRequestException("ItemId should not be empty or null");
            if(Body.StoreId is null || Body.StoreId == 0)
                throw new HttpRequestException("StoreIds should not be empty or null");
            if(ItemStoreRepository.GetByRelationship((long)Body.ItemId, (long)Body.StoreId) is not null)
                throw new HttpRequestException("Item already registered in this store");

            // GET ITEM
            Item? Item = ItemAppService.GetEntity(Body.ItemId);
            if(Item is null)
                throw new KeyNotFoundException("Item not found");

            // GET STORES
            Store? Stores = StoreAppService.GetEntity((long)Body.StoreId);
            if(Stores is null)
                throw new KeyNotFoundException("Stores not found");

            // CREATE REGISTER
            ItemStore ItemStore = new()
            {
                Item = Item,
                Store = Stores
            };
            ItemStoreRepository.Create(ItemStore);

            // SAVE CHANGES
            UnitOfWork.SaveChanges();
        }
    }
}