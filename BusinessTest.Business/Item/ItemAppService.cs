namespace BusinessTest.Business.Item
{
    using BusinessTest.Business.Item.Models;
    using BusinessTest.Data.Repositories;
    using BusinessTest.Data.Repositories.Item;
    using BusinessTest.Entity.Item;

    public class ItemAppService : BaseAppService, IItemAppService
    {
        private readonly IItemRepository ItemRepository;

        public ItemAppService(
            IUnitOfWork unitOfWork,
            IItemRepository itemRepository
        ) : base(unitOfWork) => (ItemRepository) = (itemRepository);

        public void UpdateEntity(Item item) => ItemRepository.Update(item);

        public Item? GetEntity(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            return ItemRepository.Get((long)Id);
        }

        public void Delete(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Item? Item = ItemRepository.Get((long)Id);
            if (Item is null)
                throw new KeyNotFoundException("Item not found");

            // SAVE CHANGES
            ItemRepository.Delete(Item);
            UnitOfWork.SaveChanges();
        }

        public ItemResponseModelDto Get(long? Id)
        {
            //
            if (Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Item? Item = ItemRepository.Get((long)Id);
            if (Item is null)
                throw new KeyNotFoundException("Item not found");

            ItemResponseModelDto ItemModel = new()
            {
                Id = Item.Id,
                Code = Item.Code,
                Description = Item.Description,
                Price = Item.Price,
                Image = Item.Image,
                Stock = Item.Stock
            };
            return ItemModel;
        }

        public List<ItemResponseModelDto> GetAll()
        {
            List<ItemResponseModelDto> Items = new();
            ItemRepository.GetAll()?.ForEach(Item => Items.Add(new()
            {
                Id = Item.Id,
                Code = Item.Code,
                Description = Item.Description,
                Price = Item.Price,
                Image = Item.Image,
                Stock = Item.Stock
            }));
            return Items;
        }

        public void Update(long? Id, ItemRequestModelDto Body)
        {
            //
            if(Id is null || Id == 0)
                throw new HttpRequestException("Id should not be empty or null");

            Item? Item = ItemRepository.Get((long)Id);
            if(Item is null)
                throw new KeyNotFoundException("Item not found");

            // SET CHANGES
            Item.Code = Body.Code is not null ? Body.Code : Item.Code;
            Item.Description = Body.Description is not null ? Body.Description : Item.Description;
            Item.Price = Body.Price is not null ? Body.Price : Item.Price;
            Item.Image = Body.ImageBase64 is not null ? Body.ImageBase64 : Item.Image;
            Item.Stock = Body.Stock is not null ? (long)Body.Stock : Item.Stock;

            // SAVE CHANGES
            ItemRepository.Update(Item);
            UnitOfWork.SaveChanges();
        }

        public void Create(ItemRequestModelDto Body)
        {
            //
            if(string.IsNullOrEmpty(Body.Code))
                throw new HttpRequestException("Code should not be empty or null");

            Item Item = new()
            {
                Code = Body.Code,
                Description = Body.Description is not null ? Body.Description : null,
                Price = Body.Price is not null ? Body.Price : 0,
                Image = Body.ImageBase64 is not null ? Body.ImageBase64: null,
                Stock = Body.Stock is not null ? (long)Body.Stock : 0
            };

            // SAVE CHANGES
            ItemRepository.Create(Item);
            UnitOfWork.SaveChanges();
        }
    }
}