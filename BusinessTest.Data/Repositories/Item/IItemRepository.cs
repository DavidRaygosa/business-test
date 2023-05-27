namespace BusinessTest.Data.Repositories.Item
{
    using BusinessTest.Entity.Item;

    public interface IItemRepository
    {
        void Delete(Item Item);
        List<Item>? GetAll();
        void Update(Item Item);
        Item? Get(long Id);
        void Create(Item Item);
    }
}