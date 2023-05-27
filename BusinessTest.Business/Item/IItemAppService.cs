namespace BusinessTest.Business.Item
{
    using BusinessTest.Business.Item.Models;
    using BusinessTest.Entity.Item;

    public interface IItemAppService
    {
        void Delete(long? id);
        ItemResponseModelDto Get(long? id);
        Item? GetEntity(long? id);
        void UpdateEntity(Item item);
        List<ItemResponseModelDto> GetAll();
        void Update(long? Id, ItemRequestModelDto Body);
        void Create(ItemRequestModelDto Body);
    }
}